Imports System.Drawing

Public Class frmDataHandlingPractice
    Inherits Form

    Private ReadOnly firstNumberInput As New TextBox With {.Location = New Point(30, 100), .Width = 180, .Font = New Font("Segoe UI", 12.0!)}
    Private ReadOnly secondNumberInput As New TextBox With {.Location = New Point(270, 100), .Width = 180, .Font = New Font("Segoe UI", 12.0!)}
    Private ReadOnly operationInput As New ComboBox With {.Location = New Point(30, 180), .Width = 180, .DropDownStyle = ComboBoxStyle.DropDownList, .Font = New Font("Segoe UI", 11.0!)}
    Private ReadOnly resultLabel As New Label With {.Location = New Point(30, 260), .Size = New Size(430, 42), .Font = New Font("Segoe UI", 13.0!, FontStyle.Bold), .ForeColor = Color.White, .TextAlign = ContentAlignment.MiddleLeft}

    Public Sub New()
        Text = "Week 5 - Data Handling Calculator"
        ClientSize = New Size(500, 340)
        StartPosition = FormStartPosition.CenterParent
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        BackColor = Color.FromArgb(11, 27, 49)
        ForeColor = Color.White

        Controls.Add(New Label With {.Text = "DATA HANDLING CALCULATOR", .Location = New Point(30, 24), .Size = New Size(430, 38), .Font = New Font("Segoe UI", 18.0!, FontStyle.Bold), .ForeColor = Color.White})
        Controls.Add(New Label With {.Text = "Enter two values and choose an operation.", .Location = New Point(30, 64), .Size = New Size(430, 24), .Font = New Font("Segoe UI", 10.0!), .ForeColor = Color.FromArgb(190, 208, 228)})
        Controls.Add(New Label With {.Text = "First number", .Location = New Point(30, 78), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(firstNumberInput)
        Controls.Add(New Label With {.Text = "Second number", .Location = New Point(270, 78), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(secondNumberInput)
        operationInput.Items.AddRange(New Object() {"Add", "Subtract", "Multiply", "Divide"})
        operationInput.SelectedIndex = 0
        Controls.Add(New Label With {.Text = "Operation", .Location = New Point(30, 155), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(operationInput)

        Dim calculateButton As New Button With {.Text = "Calculate", .Location = New Point(270, 178), .Size = New Size(180, 38), .BackColor = Color.FromArgb(37, 99, 157), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)}
        AddHandler calculateButton.Click, AddressOf Calculate
        Controls.Add(calculateButton)
        Controls.Add(New Panel With {.Location = New Point(30, 235), .Size = New Size(420, 1), .BackColor = Color.FromArgb(70, 95, 124)})
        Controls.Add(resultLabel)
    End Sub

    Private Sub Calculate(sender As Object, e As EventArgs)
        Dim firstValue As Decimal
        Dim secondValue As Decimal
        If Not Decimal.TryParse(firstNumberInput.Text, firstValue) OrElse Not Decimal.TryParse(secondNumberInput.Text, secondValue) Then
            resultLabel.Text = "Enter valid numbers in both fields."
            Return
        End If

        Dim result As Decimal
        Select Case CStr(operationInput.SelectedItem)
            Case "Add"
                result = firstValue + secondValue
            Case "Subtract"
                result = firstValue - secondValue
            Case "Multiply"
                result = firstValue * secondValue
            Case "Divide"
                If secondValue = 0D Then
                    resultLabel.Text = "Division by zero is not allowed."
                    Return
                End If
                result = firstValue / secondValue
            Case Else
                resultLabel.Text = "Select an operation."
                Return
        End Select

        resultLabel.Text = $"Result: {result}"
    End Sub
End Class
