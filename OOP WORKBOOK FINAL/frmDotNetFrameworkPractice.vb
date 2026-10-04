Public Class frmDotNetFrameworkPractice
    Inherits Form

    Private ReadOnly radiusInput As New TextBox With {.Location = New Point(24, 48), .Width = 180}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 135), .Size = New Size(380, 45), .ForeColor = Color.White}

    Public Sub New()
        Text = ".NET Framework Practice"
        ClientSize = New Size(440, 220)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        Controls.Add(New Label With {.Text = "Circle radius:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(radiusInput)

        Dim computeButton As New Button With {.Text = "Compute Area", .Location = New Point(24, 95), .Width = 120, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler computeButton.Click, AddressOf ComputeArea
        Controls.Add(computeButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub ComputeArea(sender As Object, e As EventArgs)
        Dim radius As Double
        If Not Double.TryParse(radiusInput.Text, radius) OrElse radius < 0 Then
            outputLabel.Text = "Enter a valid non-negative radius."
            Return
        End If

        Dim area As Double = Math.PI * Math.Pow(radius, 2)
        outputLabel.Text = $"Area: {area:F2}"
    End Sub
End Class
