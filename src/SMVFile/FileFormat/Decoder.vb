Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging.Drawing2D.Colors.Scaler
Imports Microsoft.VisualBasic.Math.LinearAlgebra
Imports Microsoft.VisualBasic.Math.Quantile

Namespace FileFormat

    Public Module Decoder

        <Extension>
        Public Function Decode(data As Double(), header As Header) As Double()
            Dim q = data.FindThreshold(0.6)
            Dim v As New Vector(data)

            v(v > q) = Vector.Scalar(q)

            Return v.ToArray
        End Function
    End Module
End Namespace