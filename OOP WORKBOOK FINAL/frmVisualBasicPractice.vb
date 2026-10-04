Imports System.Drawing

Public Class frmVisualBasicPractice
    Inherits Form

    Private ReadOnly firstNumberInput As New TextBox With {.Location = New Point(24, 45), .Width = 200}
    Private ReadOnly secondNumberInput As New TextBox With {.Location = New Point(24, 95), .Width = 200}
    Private ReadOnly operationInput As New ComboBox With {.Location = New Point(250, 45), .Width = 150, .DropDownStyle = ComboBoxStyle.DropDownList}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 175), .AutoSize = True, .ForeColor = Color.White}

    Public Sub New()
        Text = "Visual Basic .NET Practice"
        ClientSize = New Size(450, 240)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        ForeColor = Color.White

        Controls.Add(New Label With {.Text = "First number:", .Location = New Point(24, 20), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(firstNumberInput)
        Controls.Add(New Label With {.Text = "Second number:", .Location = New Point(24, 70), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(secondNumberInput)
        Controls.Add(New Label With {.Text = "Operation:", .Location = New Point(250, 20), .AutoSize = True, .ForeColor = Color.White})
        operationInput.Items.AddRange(New Object() {"Add", "Subtract", "Multiply", "Divide"})
        operationInput.SelectedIndex = 0
        Controls.Add(operationInput)

        Dim runButton As New Button With {.Text = "Compute", .Location = New Point(24, 135), .Width = 110, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler runButton.Click, AddressOf ComputeResult
        Controls.Add(runButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub ComputeResult(sender As Object, e As EventArgs)
        Dim first As Decimal
        Dim second As Decimal
        If Not Decimal.TryParse(firstNumberInput.Text, first) OrElse Not Decimal.TryParse(secondNumberInput.Text, second) Then
            outputLabel.Text = "Enter valid numbers in both fields."
            Return
        End If

        Dim result As Decimal
        Select Case CStr(operationInput.SelectedItem)
            Case "Add"
                result = first + second
            Case "Subtract"
                result = first - second
            Case "Multiply"
                result = first * second
            Case "Divide"
                If second = 0D Then
                    outputLabel.Text = "Cannot divide by zero."
                    Return
                End If
                result = first / second
            Case Else
                outputLabel.Text = "Select an operation."
                Return
        End Select

        outputLabel.Text = $"Result: {result}"
    End Sub
End Class
