Imports System.IO
Imports Microsoft.VisualBasic.Data.IO

Namespace FileFormat

    Public Class Reader

        ReadOnly buf As BinaryDataReader
        ReadOnly header_size As Integer
        ReadOnly header As Header

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

        End Sub
    End Class
End Namespace