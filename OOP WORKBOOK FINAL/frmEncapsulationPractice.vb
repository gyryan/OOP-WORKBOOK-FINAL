Imports System.Drawing

Public Class frmEncapsulationPractice
    Inherits Form

    Private ReadOnly amountInput As New TextBox With {.Location = New Point(24, 45), .Width = 220}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 130), .AutoSize = True}
    Private ReadOnly account As New BankAccount()

    Public Sub New()
        Text = "Encapsulation Practice"
        ClientSize = New Size(400, 210)
        StartPosition = FormStartPosition.CenterParent
        Controls.Add(New Label With {.Text = "Deposit amount:", .Location = New Point(24, 20), .AutoSize = True})
        Controls.Add(amountInput)
        Dim depositButton As New Button With {.Text = "Deposit", .Location = New Point(24, 85), .Width = 100}
        AddHandler depositButton.Click, AddressOf Deposit
        Controls.Add(depositButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub Deposit(sender As Object, e As EventArgs)
        Dim amount As Decimal
        If Not Decimal.TryParse(amountInput.Text, amount) OrElse amount <= 0D Then
            outputLabel.Text = "Enter a positive amount."
            Return
        End If

        account.Deposit(amount)
        outputLabel.Text = $"Protected balance: {account.GetBalance():C2}"
        amountInput.Clear()
    End Sub

    Private Class BankAccount
        Private balance As Decimal

        Public Sub Deposit(amount As Decimal)
            If amount > 0D Then balance += amount
        End Sub

        Public Function GetBalance() As Decimal
            Return balance
        End Function
    End Class
End Class
