Imports System.Reflection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.DataFramework
Imports Microsoft.VisualBasic.Data.IO
Imports Microsoft.VisualBasic.Serialization.JSON
Imports NRRD
Imports any = Microsoft.VisualBasic.Scripting

Namespace FileFormat

    Public Class Header

        Public Property HEADER_BYTES As Integer
        Public Property [DIM] As Integer
        Public Property BYTE_ORDER As String
        Public Property TYPE As String
        Public Property SIZE1 As Integer
        Public Property SIZE2 As Integer
        Public Property PIXEL_SIZE As Double
        Public Property BIN As String
        Public Property BIN_TYPE As String
        Public Property ADC As String
        Public Property CREV As Double
        Public Property BEAMLINE As String
        Public Property DETECTOR_SN As Integer
        Public Property [DATE] As String
        Public Property TIME As Double
        Public Property DISTANCE As Double
        Public Property TWOTHETA As Double
        Public Property PHI As Double
        Public Property OSC_START As Double
        Public Property OSC_RANGE As Double
        Public Property WAVELENGTH As Double
        Public Property BEAM_CENTER_X As Double
        Public Property BEAM_CENTER_Y As Double
        Public Property DENZO_X_BEAM As Double
        Public Property DENZO_Y_BEAM As Double

        Sub New()
        End Sub

        Public Function ToNrrdMetadata() As Metadata
            Return New Metadata With {
                .dimension = [DIM],
                .sizes = {SIZE1, SIZE2},
                .encoding = Encoding.ascii,
                .endian = If(BYTE_ORDER.TextEquals("little_endian"), ByteOrder.LittleEndian, ByteOrder.BigEndian)
            }
        End Function

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

        Public Shared Function Load(metadata As Dictionary(Of String, String)) As Header
            Dim header As New Header

            Static writers As Dictionary(Of String, PropertyInfo) = GetType(Header) _
                .GetProperties(PublicProperty) _
                .Where(Function(pi) pi.CanWrite AndAlso pi.GetIndexParameters.IsNullOrEmpty) _
                .ToDictionary(Function(pi)
                                  Return pi.Name
                              End Function)

            For Each tuple As KeyValuePair(Of String, String) In metadata
                If writers.ContainsKey(tuple.Key) Then
                    writers(tuple.Key).SetValue(header, value:=any.CTypeDynamic(
                            expression:=tuple.Value,
                            target:=writers(tuple.Key).PropertyType
                        )
                    )
                End If
            Next

            Return header
        End Function
    End Class
End Namespace