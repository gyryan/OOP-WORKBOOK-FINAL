Public Class frmDebuggingAndTracingPractice
    Inherits Form

    Private ReadOnly dividendInput As New TextBox With {.Location = New Point(24, 48), .Width = 180}
    Private ReadOnly divisorInput As New TextBox With {.Location = New Point(24, 98), .Width = 180}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 175), .Size = New Size(400, 55), .ForeColor = Color.White}

    Public Sub New()
        Text = "Debugging and Tracing Practice"
        ClientSize = New Size(460, 260)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        Controls.Add(New Label With {.Text = "Dividend:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(dividendInput)
        Controls.Add(New Label With {.Text = "Divisor:", .Location = New Point(24, 73), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(divisorInput)

        Dim runButton As New Button With {.Text = "Trace Division", .Location = New Point(24, 135), .Width = 130, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler runButton.Click, AddressOf TraceDivision
        Controls.Add(runButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub TraceDivision(sender As Object, e As EventArgs)
        Dim dividend As Decimal
        Dim divisor As Decimal
        If Not Decimal.TryParse(dividendInput.Text, dividend) OrElse Not Decimal.TryParse(divisorInput.Text, divisor) Then
            outputLabel.Text = "Enter valid numbers."
            Return
        End If

        Try
            Dim result As Decimal = dividend / divisor
            outputLabel.Text = $"Trace: dividend={dividend}, divisor={divisor}. Result={result}."
        Catch ex As DivideByZeroException
            outputLabel.Text = $"Run-time error handled: divisor={divisor}. Division by zero is not allowed."
        End Try
    End Sub
End Class
