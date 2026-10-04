Imports System.Drawing

Public Class frmDataHandlingPractice
    Inherits Form

    Private ReadOnly valueInput As New TextBox With {.Location = New Point(24, 48), .Width = 250}
    Private ReadOnly typeInput As New ComboBox With {.Location = New Point(24, 105), .Width = 180, .DropDownStyle = ComboBoxStyle.DropDownList}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 190), .AutoSize = True, .ForeColor = Color.White}

    Public Sub New()
        Text = "Data Handling Practice"
        ClientSize = New Size(430, 250)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        Controls.Add(New Label With {.Text = "Value:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(valueInput)
        Controls.Add(New Label With {.Text = "Data type:", .Location = New Point(24, 80), .AutoSize = True, .ForeColor = Color.White})
        typeInput.Items.AddRange(New Object() {"String", "Integer", "Decimal"})
        typeInput.SelectedIndex = 0
        Controls.Add(typeInput)

        Dim storeButton As New Button With {.Text = "Store Value", .Location = New Point(24, 150), .Width = 110, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler storeButton.Click, AddressOf StoreValue
        Controls.Add(storeButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub StoreValue(sender As Object, e As EventArgs)
        Select Case CStr(typeInput.SelectedItem)
            Case "String"
                Dim storedValue As String = valueInput.Text
                outputLabel.Text = "Stored String: " & storedValue
            Case "Integer"
                Dim storedValue As Integer
                If Not Integer.TryParse(valueInput.Text, storedValue) Then
                    outputLabel.Text = "Enter a valid whole number."
                    Return
                End If
                outputLabel.Text = "Stored Integer: " & storedValue.ToString()
            Case "Decimal"
                Dim storedValue As Decimal
                If Not Decimal.TryParse(valueInput.Text, storedValue) Then
                    outputLabel.Text = "Enter a valid decimal number."
                    Return
                End If
                outputLabel.Text = "Stored Decimal: " & storedValue.ToString()
        End Select
    End Sub
End Class
