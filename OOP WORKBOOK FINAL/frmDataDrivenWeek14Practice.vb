Public Class frmDataDrivenWeek14Practice
    Inherits Form

    Private ReadOnly nameInput As New TextBox With {.Location = New Point(24, 48), .Width = 220}
    Private ReadOnly notesInput As New TextBox With {.Location = New Point(270, 48), .Width = 220}
    Private ReadOnly records As New DataTable("Records")
    Private ReadOnly recordGrid As New DataGridView With {.Location = New Point(24, 135), .Size = New Size(500, 220), .ReadOnly = True, .AllowUserToAddRows = False, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
    Private ReadOnly statusLabel As New Label With {.Location = New Point(24, 100), .AutoSize = True, .ForeColor = Color.White}

    Public Sub New()
        Text = "Data-Driven Applications Week 14 Practice"
        ClientSize = New Size(560, 390)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        records.Columns.Add("Name", GetType(String))
        Dim notesColumn As DataColumn = records.Columns.Add("Notes", GetType(String))
        notesColumn.AllowDBNull = True
        recordGrid.DataSource = records

        Controls.Add(New Label With {.Text = "Name:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(nameInput)
        Controls.Add(New Label With {.Text = "Notes (optional):", .Location = New Point(270, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(notesInput)
        Dim addButton As New Button With {.Text = "Add DataRow", .Location = New Point(24, 93), .Width = 120, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler addButton.Click, AddressOf AddRecord
        Controls.Add(addButton)
        Controls.Add(statusLabel)
        Controls.Add(recordGrid)
    End Sub

    Private Sub AddRecord(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(nameInput.Text) Then
            statusLabel.Text = "Enter a name."
            Return
        End If

        Dim row As DataRow = records.NewRow()
        row("Name") = nameInput.Text.Trim()
        If String.IsNullOrWhiteSpace(notesInput.Text) Then
            row("Notes") = DBNull.Value
        Else
            row("Notes") = notesInput.Text.Trim()
        End If
        records.Rows.Add(row)
        statusLabel.Text = $"Row added to the DataTable. Rows: {records.Rows.Count}."
        nameInput.Clear()
        notesInput.Clear()
    End Sub
End Class
