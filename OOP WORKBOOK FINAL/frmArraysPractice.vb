Imports System.Drawing
Imports System.Text.RegularExpressions

Public Class frmArraysPractice
    Inherits Form

    Private ReadOnly valuesInput As New TextBox With {.Location = New Point(24, 58), .Width = 570}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 155), .Size = New Size(580, 55), .ForeColor = Color.White}

    Public Sub New()
        Text = "Arrays Practice"
        ClientSize = New Size(650, 240)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        Controls.Add(New Label With {.Text = "Type a complete Integer array declaration, including braces:", .Location = New Point(24, 28), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(valuesInput)

        Dim processButton As New Button With {.Text = "Process Array", .Location = New Point(24, 95), .Width = 120, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler processButton.Click, AddressOf ProcessArray
        Controls.Add(processButton)
        Controls.Add(outputLabel)
    End Sub

    Private Sub ProcessArray(sender As Object, e As EventArgs)
        Dim declarationMatch As Match = Regex.Match(valuesInput.Text,
            "^\s*Dim\s+[A-Za-z_][A-Za-z0-9_]*\s*(?:\(\s*\)\s+As\s+Integer|As\s+Integer\s*\(\s*\))\s*=\s*\{\s*([^{}]+?)\s*\}\s*$",
            RegexOptions.IgnoreCase)
        If Not declarationMatch.Success Then
            outputLabel.Text = "Enter a complete Integer array declaration with values inside braces."
            Return
        End If

        Dim parts As String() = declarationMatch.Groups(1).Value.Split(","c)
        Dim values(parts.Length - 1) As Integer
        For index As Integer = 0 To parts.Length - 1
            If Not Integer.TryParse(parts(index).Trim(), values(index)) Then
                outputLabel.Text = "Every item must be a valid whole number."
                Return
            End If
        Next

        Dim total As Integer = 0
        For Each value As Integer In values
            total += value
        Next

        Dim average As Decimal = CDec(total) / values.Length
        outputLabel.Text = $"Array: {String.Join(", ", values)} | Sum: {total} | Average: {average}"
    End Sub
End Class
