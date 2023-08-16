Imports System.IO
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.Text
Imports Microsoft.VisualBasic.Text.Parser

Namespace FileFormat

    Public Class Reader : Implements IDisposable

        ReadOnly buf As BinaryDataReader

        Dim header_size As Integer
        Dim header As Header
        Private disposedValue As Boolean

        Sub New(file As Stream)
            buf = New BinaryDataReader(file, Encodings.ASCII)
        End Sub

        Public Function ReadHeader() As Header
            If header Is Nothing Then
                Call buf.Seek(Scan0, SeekOrigin.Begin)
                Call loadHeaderInternal()
            End If

            Return header
        End Function

        Public Function ReadData()
            Call buf.Seek(header_size, SeekOrigin.Begin)

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
                        ' do nothing
                    Else
                        tuple = str.ToString.GetTagValue("=", trim:=True)
                        metadata.Add(tuple.Name, tuple.Value.TrimEnd(";"c, " "c, ASCII.TAB))
                    End If

                    str *= 0
                End If

                If c = "}"c Then
                    Exit Do
                End If
            Loop

            header = Header.Load(metadata)
            header_size = header.header_bytes
        End Sub

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 释放托管状态(托管对象)
                    Call buf.Dispose()
                End If

                ' TODO: 释放未托管的资源(未托管的对象)并重写终结器
                ' TODO: 将大型字段设置为 null
                disposedValue = True
            End If
        End Sub

        ' ' TODO: 仅当“Dispose(disposing As Boolean)”拥有用于释放未托管资源的代码时才替代终结器
        ' Protected Overrides Sub Finalize()
        '     ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
        '     Dispose(disposing:=False)
        '     MyBase.Finalize()
        ' End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace