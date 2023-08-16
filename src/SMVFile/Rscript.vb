
Imports Microsoft.VisualBasic.CommandLine.Reflection
Imports Microsoft.VisualBasic.Scripting.MetaData
Imports SMRUCC.Rsharp.Runtime
Imports SMRUCC.Rsharp.Runtime.Interop

<Package("Reader")>
Public Module Rscript

    <ExportAPI("open.smv_file")>
    Public Function openSMVFile(<RRawVectorArgument> file As Object, Optional env As Environment = Nothing)

    End Function
End Module
