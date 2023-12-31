
Imports System.IO
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports NRRD
Imports SMRUCC.Rsharp.Runtime
Imports SMRUCC.Rsharp.Runtime.Components
Imports SMRUCC.Rsharp.Runtime.Internal.[Object]
Imports SMRUCC.Rsharp.Runtime.Internal.Object.Converts
Imports SMRUCC.Rsharp.Runtime.Interop
Imports SMVFile.FileFormat

<Package("Reader")>
Public Module Rscript

    <ExportAPI("open.smv_file")>
    <RApiReturn(GetType(Reader))>
    Public Function openSMVFile(<RRawVectorArgument> file As Object, Optional env As Environment = Nothing) As Object
        Dim buf = SMRUCC.Rsharp.GetFileStream(file, FileAccess.Read, env)

        If buf Like GetType(Message) Then
            Return buf.TryCast(Of Message)
        End If

        Return New Reader(buf.TryCast(Of Stream))
    End Function

    <ExportAPI("smv_header")>
    Public Function GetFileHeaders(file As Reader, Optional env As Environment = Nothing) As Object
        Return RConversion.asList(file.ReadHeader, list.empty, env)
    End Function

    <ExportAPI("smv_raster")>
    <RApiReturn(GetType(RasterObject))>
    Public Function getRaster(file As Reader, Optional env As Environment = Nothing) As Object
        Dim metadata = file.ReadHeader
        Dim stream As Double() = file.ReadData.Decode(metadata)
        Dim raster As RasterObject = RasterObject.CreateRasterObject(metadata.ToNrrdMetadata, rawdata:=stream)

        Return raster
    End Function
End Module
