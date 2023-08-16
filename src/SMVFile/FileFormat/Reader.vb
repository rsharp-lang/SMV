Imports System.IO
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.Text
Imports Microsoft.VisualBasic.Text.Parser

Namespace FileFormat

    Public Class Reader

        ReadOnly buf As BinaryDataReader
        ReadOnly header_size As Integer

        Dim header As Header

        Sub New(file As Stream)
            buf = New BinaryDataReader(file)
        End Sub

        Public Function ReadHeader() As Header
            If header Is Nothing Then
                Call buf.Seek(Scan0, SeekOrigin.Begin)
                Call loadHeaderInternal()
            End If

            Return header
        End Function

        Private Sub loadHeaderInternal()
            Dim c As Char
            Dim metadata As New Dictionary(Of String, String)
            Dim str As New CharBuffer
            Dim tuple As NamedValue(Of String)

            Do While True
                c = buf.ReadChar

                If c <> ASCII.CR AndAlso c <> ASCII.LF Then
                    str += c
                Else
                    If str = "{" OrElse str = "}" Then
                        str *= 0
                    Else
                        tuple = str.ToString.GetTagValue("=", trim:=True)
                        metadata.Add(tuple.Name, tuple.Value)
                    End If
                End If

                If buf.Position = header_size AndAlso c = "}"c Then
                    Exit Do
                End If
            Loop

            header = Header.Load(metadata)
        End Sub
    End Class
End Namespace