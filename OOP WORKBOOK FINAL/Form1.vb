Imports System.Drawing
Imports System.Linq

Public Class Form1
    Private currentLessonNavigationIndex As Integer = -1
    Private currentPracticeForm As Form
    Private ReadOnly lessonNavigationOrder As String() = {
        "Classes and Objects",
        "Encapsulation",
        "Inheritance",
        "Polymorphism",
        "Week 3 : Getting Started With Microsoft Visual Basic",
        "Week 4 : Planning Application Designing Interface",
        "Lesson 5 - Data Handling",
        "Lesson 6 - The Selection and Repetition Structure",
        "Lesson 7 - Arrays",
        "Lesson 8 - Working with Controls and Properties",
        "Week 10 - Debugging and Tracing",
        "Week 11 - .NET Framework",
        "Week 12 - Multi Document Interface and String Comparison",
        "Week 13 - Database Concepts and VB.NET Database Connection",
        "Week 14 - Developing Data-Driven Applications",
        "Week 15 - Developing Data-Driven Applications: Updating Data Sources"
    }

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "PFP101 - OBJECT ORIENTED PROGRAMMING             SONGALIA, MARK RYAN O.            SBIT-2E"
        Label1.Visible = True
        Label1.Text = "PFP101 - Object Oriented Programming"
        Label1.AutoSize = False
        Label1.TextAlign = ContentAlignment.MiddleCenter
        Label1.ForeColor = Color.White
        panelHome.Dock = DockStyle.Fill
        Me.BackColor = Color.FromArgb(5, 14, 35)
        panelHome.BackColor = Color.FromArgb(5, 14, 35)
        IntroductionToolStripMenuItem.Text = "Week 1 : Orientation"
        ToolStripMenuItem2.Text = "Week 2 : Introduction to OOP"
        APPLICATIONSANDDESIGNINGINTERFACESToolStripMenuItem.Text = "Week 3 : Getting Started With Microsoft Visual Basic"
        ToolStripMenuItem3.Text = "Week 4 : Planning Application Designing Interface"
        DATAHANDLINGToolStripMenuItem.Text = "Week 5 : Data Handling"
        TheSelectionAndRepetitionStructureToolStripMenuItem.Text = "Week 6 : The Selection and Repetition Structure"
        ARRAYSToolStripMenuItem.Text = "Week 7 : Arrays"
        WorkingWithControlsAndPropertiesToolStripMenuItem.Text = "Week 8 : Working With Control Properties"
        ToolStripMenuItem4.Text = "Week 9 : MIDTERMS"
        DebuggingAndTracingToolStripMenuItem.Text = "Week 10 : Debugging and Tracing"
        ToolStripMenuItem5.Text = "Week 11 : Working with .NET Framework and Multi Document Interface"
        ToolStripMenuItem6.Text = "Week 12 : Working with .NET Framework and Multi Document Interface"
        DatabaseConnectionToolStripMenuItem.Text = "Week 13 : Database Connection"
        ToolStripMenuItem7.Text = "Week 14 : Database Connection"
        DevelopingDataDrivenApplicationToolStripMenuItem.Text = "Week 15 : Developing Data Driven Applications"
        PresentationToolStripMenuItem.Text = "Week 16 : Presentation"
        PresentationToolStripMenuItem1.Text = "Week 17 : Presentation"
        ToolStripMenuItem8.Text = "Week 18 : Final Examination"
        AnimationToolStripMenuItem.Text = "Week 19 : Animation"
        ExitToolStripMenuItem1.Text = "Week 21 : Exit"
        LessonsToolStripMenuItem.Visible = False
        LessonsToolStripMenuItem1.Visible = False
        panelHome.BackgroundImageLayout = ImageLayout.Stretch
        MenuStrip1.ShowItemToolTips = True
        HelpToolStripMenuItem.ToolTipText = "Learn about the PFP101 Object Oriented Programming application."
        homeLessonList.SelectedIndex = 0
        oopTopicList.SelectedIndex = 0
        homeSelectLessonLabel.Visible = True
        homeLessonList.Visible = True
        oopTopicLabel.Visible = False
        oopTopicList.Visible = False
        studentNameLabel.Visible = False
        classNameLabel.Visible = False
        lessonDefinitionBox.Visible = False
        lessonReferenceBox.Visible = False
        lessonPlaceholderLabel.Visible = False
        backButton.Visible = False
        previousLessonButton.Visible = False
        practiceOutputButton.Visible = False
        nextLessonButton.Visible = False
        UpdateHomeLayout()
    End Sub

    Private Sub UpdateHomeLayout()
        If homeLessonList Is Nothing Then Return

        Dim panelWidth As Integer = panelHome.ClientSize.Width
        Dim titleWidth As Integer = Math.Max(300, panelWidth - 80)
        Label1.Size = New Size(titleWidth, 62)
        Label1.Location = New Point(Math.Max(0, (panelWidth - titleWidth) \ 2), 38)

        Dim selectorWidth As Integer = Math.Min(760, Math.Max(260, panelWidth - 100))
        MenuStrip1.Width = panelWidth
        homeSelectLessonLabel.Size = New Size(selectorWidth, 34)
        homeSelectLessonLabel.Location = New Point(Math.Max(0, (panelWidth - selectorWidth) \ 2), Label1.Bottom + 22)
        homeLessonList.Size = New Size(selectorWidth, 44)
        homeLessonList.DropDownWidth = selectorWidth
        homeLessonList.Location = New Point(Math.Max(0, (panelWidth - selectorWidth) \ 2), homeSelectLessonLabel.Bottom + 8)

        Dim detailTop As Integer = homeLessonList.Bottom + 32
        If oopTopicLabel IsNot Nothing Then
            oopTopicLabel.Size = New Size(selectorWidth, 28)
            oopTopicLabel.Location = New Point(Math.Max(0, (panelWidth - selectorWidth) \ 2), homeLessonList.Bottom + 8)
            oopTopicList.Size = New Size(selectorWidth, 40)
            oopTopicList.DropDownWidth = selectorWidth
            oopTopicList.Location = New Point(Math.Max(0, (panelWidth - selectorWidth) \ 2), oopTopicLabel.Bottom + 2)
            If oopTopicList.Visible Then detailTop = oopTopicList.Bottom + 28
        End If
        Dim detailWidth As Integer = Math.Min(420, Math.Max(160, (panelWidth - 100) \ 2))
        studentNameLabel.Size = New Size(detailWidth, 34)
        studentNameLabel.Location = New Point(50, detailTop)
        classNameLabel.Size = New Size(detailWidth, 34)
        classNameLabel.Location = New Point(Math.Max(50, panelWidth - detailWidth - 50), detailTop)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        UpdateHomeLayout()
        UpdateLessonContentLayout()
        UpdateLessonNavigationLayout()
        If panelHome IsNot Nothing Then panelHome.Invalidate()
    End Sub

    Private Sub HomeLessonList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles homeLessonList.SelectedIndexChanged
        oopTopicLabel.Visible = False
        oopTopicList.Visible = False
        Select Case homeLessonList.SelectedIndex - 1
            Case -1
                Return
            Case 0
                IntroductionToolStripMenuItem.PerformClick()
            Case 1
                oopTopicLabel.Visible = True
                oopTopicList.Visible = True
                oopTopicList.SelectedIndex = 0
                UpdateHomeLayout()
                Return
            Case 2
                APPLICATIONSANDDESIGNINGINTERFACESToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 3
                PlanningApplicationsToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 4
                DATAHANDLINGToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 5
                TheSelectionAndRepetitionStructureToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 6
                ARRAYSToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 7
                WorkingWithControlsAndPropertiesToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 8
                ToolStripMenuItem4.PerformClick()
            Case 9
                DebuggingAndTracingToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 10
                DotNetFrameworkToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 11
                MdiAndStringComparisonToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 12
                DatabaseConnectionToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 13
                DataDrivenWeek14ToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 14
                DataDrivenWeek15ToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 15
                PresentationToolStripMenuItem.PerformClick()
            Case 16
                PresentationToolStripMenuItem1.PerformClick()
            Case 17
                ToolStripMenuItem8.PerformClick()
            Case 18
                AnimationToolStripMenuItem.PerformClick()
            Case 19
                DataDrivenWeek15ToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 20
                ExItToolStripMenuItem_Click(Me, EventArgs.Empty)
        End Select
    End Sub

    Private Sub OopTopicList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles oopTopicList.SelectedIndexChanged
        Select Case oopTopicList.SelectedIndex
            Case 1
                ClassesAndObjectsToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 2
                EncapsulationToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 3
                InheritanceToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case 4
                PolymorphismToolStripMenuItem_Click(Me, EventArgs.Empty)
        End Select
    End Sub

    Private Sub ShowLesson(title As String, definition As String, referenceCode As String, practiceForm As Form, Optional isTopicOutline As Boolean = False, Optional includePracticeButton As Boolean = True, Optional placeholderOnly As Boolean = False)
        currentLessonNavigationIndex = Array.IndexOf(lessonNavigationOrder, title)
        currentPracticeForm = If(includePracticeButton, practiceForm, Nothing)

        Label1.Visible = False
        homeSelectLessonLabel.Visible = False
        homeLessonList.Visible = False
        oopTopicLabel.Visible = False
        oopTopicList.Visible = False
        studentNameLabel.Visible = False
        classNameLabel.Visible = False

        lessonDefinitionBox.Visible = Not placeholderOnly
        lessonReferenceBox.Visible = Not placeholderOnly
        lessonPlaceholderLabel.Visible = placeholderOnly
        If placeholderOnly Then
            lessonPlaceholderLabel.Text = title & Environment.NewLine & Environment.NewLine & "Content coming soon"
            lessonPlaceholderLabel.BringToFront()
        Else
            lessonDefinitionBox.Text = title & Environment.NewLine & Environment.NewLine & definition
            lessonDefinitionBox.SelectAll()
            lessonDefinitionBox.SelectionAlignment = HorizontalAlignment.Center
            lessonDefinitionBox.DeselectAll()
            lessonReferenceBox.Text = referenceCode
            lessonReferenceBox.WordWrap = isTopicOutline
            lessonReferenceBox.Font = If(isTopicOutline, New Font("Segoe UI", 11.0!), New Font("Consolas", 10.0!))
            lessonDefinitionBox.BringToFront()
            lessonReferenceBox.BringToFront()
        End If

        backButton.Visible = True
        previousLessonButton.Visible = True
        nextLessonButton.Visible = True
        practiceOutputButton.Visible = includePracticeButton
        previousLessonButton.Enabled = FindNavigableLessonIndex(currentLessonNavigationIndex - 1, -1) >= 0
        nextLessonButton.Enabled = FindNavigableLessonIndex(currentLessonNavigationIndex + 1, 1) >= 0
        backButton.BringToFront()
        previousLessonButton.BringToFront()
        If includePracticeButton Then practiceOutputButton.BringToFront()
        nextLessonButton.BringToFront()

        UpdateLessonContentLayout()
        UpdateLessonNavigationLayout()
    End Sub

    Private Sub UpdateLessonContentLayout()
        Dim contentWidth As Integer = Math.Max(300, panelHome.ClientSize.Width - 160)
        lessonDefinitionBox.SetBounds(80, 45, contentWidth, 190)
        lessonReferenceBox.SetBounds(80, 250, contentWidth, Math.Max(160, panelHome.ClientSize.Height - 330))
        lessonPlaceholderLabel.SetBounds(80, 45, contentWidth, Math.Max(190, panelHome.ClientSize.Height - 130))
    End Sub

    Private Sub UpdateLessonNavigationLayout()
        If previousLessonButton Is Nothing OrElse nextLessonButton Is Nothing Then Return

        Dim spacing As Integer = 12
        Dim arrowWidth As Integer = previousLessonButton.Width
        Dim practiceWidth As Integer = If(practiceOutputButton.Visible, practiceOutputButton.Width, 0)
        Dim groupWidth As Integer = arrowWidth * 2 + spacing * If(practiceOutputButton.Visible, 2, 1) + practiceWidth
        Dim groupLeft As Integer = Math.Max(0, (panelHome.ClientSize.Width - groupWidth) \ 2)
        Dim buttonTop As Integer = panelHome.ClientSize.Height - previousLessonButton.Height - 16

        backButton.Location = New Point(24, buttonTop)
        previousLessonButton.Location = New Point(groupLeft, buttonTop)
        If practiceOutputButton.Visible Then
            practiceOutputButton.Location = New Point(groupLeft + arrowWidth + spacing, buttonTop)
            nextLessonButton.Location = New Point(practiceOutputButton.Right + spacing, buttonTop)
        Else
            nextLessonButton.Location = New Point(previousLessonButton.Right + spacing, buttonTop)
        End If
    End Sub

    Private Function FindNavigableLessonIndex(startIndex As Integer, direction As Integer) As Integer
        Dim index As Integer = startIndex
        While index >= 0 AndAlso index < lessonNavigationOrder.Length
            If lessonNavigationOrder(index) <> "Lesson 8 - Working with Controls and Properties" Then Return index
            index += direction
        End While
        Return -1
    End Function

    Private Sub NavigateLesson(direction As Integer)
        Dim targetIndex As Integer = FindNavigableLessonIndex(currentLessonNavigationIndex + direction, direction)
        If targetIndex < 0 Then Return

        Select Case lessonNavigationOrder(targetIndex)
            Case "Classes and Objects"
                ClassesAndObjectsToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Encapsulation"
                EncapsulationToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Inheritance"
                InheritanceToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Polymorphism"
                PolymorphismToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Week 3 : Getting Started With Microsoft Visual Basic"
                APPLICATIONSANDDESIGNINGINTERFACESToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Week 4 : Planning Application Designing Interface"
                PlanningApplicationsToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Lesson 5 - Data Handling"
                DATAHANDLINGToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Lesson 6 - The Selection and Repetition Structure"
                TheSelectionAndRepetitionStructureToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Lesson 7 - Arrays"
                ARRAYSToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Week 10 - Debugging and Tracing"
                DebuggingAndTracingToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Week 11 - .NET Framework"
                DotNetFrameworkToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Week 12 - Multi Document Interface and String Comparison"
                MdiAndStringComparisonToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Week 13 - Database Concepts and VB.NET Database Connection"
                DatabaseConnectionToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Week 14 - Developing Data-Driven Applications"
                DataDrivenWeek14ToolStripMenuItem_Click(Me, EventArgs.Empty)
            Case "Week 15 - Developing Data-Driven Applications: Updating Data Sources"
                DataDrivenWeek15ToolStripMenuItem_Click(Me, EventArgs.Empty)
        End Select
    End Sub

    Private Sub LessonsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LessonsToolStripMenuItem.Click
    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        ReturnToHome()
    End Sub

    Private Sub ReturnToHome()
        currentLessonNavigationIndex = -1
        currentPracticeForm = Nothing
        Label1.Visible = True
        homeSelectLessonLabel.Visible = True
        homeLessonList.Visible = True
        oopTopicLabel.Visible = False
        oopTopicList.Visible = False
        studentNameLabel.Visible = False
        classNameLabel.Visible = False
        homeLessonList.SelectedIndex = 0
        oopTopicList.SelectedIndex = 0
        lessonDefinitionBox.Visible = False
        lessonReferenceBox.Visible = False
        lessonPlaceholderLabel.Visible = False
        backButton.Visible = False
        previousLessonButton.Visible = False
        practiceOutputButton.Visible = False
        nextLessonButton.Visible = False
        UpdateHomeLayout()
        homeLessonList.BringToFront()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles backButton.Click
        ReturnToHome()
    End Sub

    Private Sub PreviousLessonButton_Click(sender As Object, e As EventArgs) Handles previousLessonButton.Click
        NavigateLesson(-1)
    End Sub

    Private Sub NextLessonButton_Click(sender As Object, e As EventArgs) Handles nextLessonButton.Click
        NavigateLesson(1)
    End Sub

    Private Sub PracticeOutputButton_Click(sender As Object, e As EventArgs) Handles practiceOutputButton.Click
        If currentPracticeForm IsNot Nothing Then
            Using currentPracticeForm
                currentPracticeForm.ShowDialog(Me)
            End Using
            currentPracticeForm = Nothing
        End If
    End Sub

    Private Sub ClassesAndObjectsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClassesAndObjectsToolStripMenuItem.Click
        ShowLesson("Classes and Objects", "Classes describe the type of objects, while objects are usable instances of classes. Creating an object is called instantiation. A class is like a blueprint, and an object is a building made from that blueprint.",
                   "Public Class Person" & Environment.NewLine & "    Public Name As String" & Environment.NewLine & "    Public Age As Integer" & Environment.NewLine & "End Class" & Environment.NewLine & Environment.NewLine & "Dim p As New Person()" & Environment.NewLine & "p.Name = ""Alice""" & Environment.NewLine & "p.Age = 30", New frmClassesAndObjectsPractice())
    End Sub

    Private Sub EncapsulationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EncapsulationToolStripMenuItem.Click
        ShowLesson("Encapsulation", "Encapsulation bundles data and the methods that operate on it in one class. It hides internal state and allows access through well-defined methods, which promotes modularity, maintainability, and data safety.",
                   "Public Class BankAccount" & Environment.NewLine & "    Private balance As Decimal" & Environment.NewLine & Environment.NewLine & "    Public Sub Deposit(amount As Decimal)" & Environment.NewLine & "        If amount > 0 Then" & Environment.NewLine & "            balance += amount" & Environment.NewLine & "        End If" & Environment.NewLine & "    End Sub" & Environment.NewLine & Environment.NewLine & "    Public Function GetBalance() As Decimal" & Environment.NewLine & "        Return balance" & Environment.NewLine & "    End Function" & Environment.NewLine & "End Class", New frmEncapsulationPractice())
    End Sub

    Private Sub InheritanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InheritanceToolStripMenuItem.Click
        ShowLesson("Inheritance", "Inheritance allows a derived class to inherit properties and behaviors from a base class. It promotes reuse, extensibility, and hierarchical relationships such as a Dog being an Animal.",
                   "Public Class Animal" & Environment.NewLine & "    Public Overridable Sub Speak()" & Environment.NewLine & "        Console.WriteLine(""Animal speaks"")" & Environment.NewLine & "    End Sub" & Environment.NewLine & "End Class" & Environment.NewLine & Environment.NewLine & "Public Class Dog" & Environment.NewLine & "    Inherits Animal" & Environment.NewLine & "    Public Overrides Sub Speak()" & Environment.NewLine & "        Console.WriteLine(""Dog barks"")" & Environment.NewLine & "    End Sub" & Environment.NewLine & "End Class", New frmInheritancePractice())
    End Sub

    Private Sub PolymorphismToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PolymorphismToolStripMenuItem.Click
        ShowLesson("Polymorphism", "Polymorphism allows objects of different classes to be treated as instances of a common base class. Method overriding allows each derived class to provide its own implementation while using the same interface.",
                   "Dim a As Animal = New Dog()" & Environment.NewLine & "a.Speak()  ' Outputs: Dog barks", New frmPolymorphismPractice())
    End Sub

    Private Sub ExItToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExItToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub APPLICATIONSANDDESIGNINGINTERFACESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles APPLICATIONSANDDESIGNINGINTERFACESToolStripMenuItem.Click
        ShowLesson("Week 3 : Getting Started With Microsoft Visual Basic",
                   "Visual Basic .NET is a programming language used to write instructions for computers. Its keywords, variables, operators, statements, and procedures follow defined syntax; the compiler translates source code for execution. Visual Studio is an IDE that provides an editor, build tools, and debugger. A console application reads and writes text through the console.",
                   "Module Program" & Environment.NewLine & "    Sub Main()" & Environment.NewLine & "        ' Read two values and calculate a sum" & Environment.NewLine & "        Dim firstNumber As Integer = 12" & Environment.NewLine & "        Dim secondNumber As Integer = 8" & Environment.NewLine & "        Dim total As Integer = firstNumber + secondNumber" & Environment.NewLine & "        Console.WriteLine(total)" & Environment.NewLine & "    End Sub" & Environment.NewLine & "End Module",
                   New frmVisualBasicPractice())
    End Sub

    Private Sub PlanningApplicationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        ShowLesson("Week 4 : Planning Application Designing Interface",
                   "A Windows Forms interface is built from objects such as forms and controls. Properties describe and configure an object's appearance or state, methods make it perform actions, and events signal that an action—such as a button click—has occurred. Event handlers contain the code that responds to those events.",
                    "Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load" & Environment.NewLine & "    lblMessage.Text = ""Ready""" & Environment.NewLine & "End Sub" & Environment.NewLine & Environment.NewLine & "Private Sub btnShowMessage_Click(sender As Object, e As EventArgs) Handles btnShowMessage.Click" & Environment.NewLine & "    lblMessage.Text = txtName.Text" & Environment.NewLine & "    MessageBox.Show(lblMessage.Text)" & Environment.NewLine & "End Sub",
                   New frmPlanningApplicationsPractice())
    End Sub

    Private Sub DATAHANDLINGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DATAHANDLINGToolStripMenuItem.Click
        ShowLesson("Lesson 5 - Data Handling",
                   "Data handling includes storing, using, and processing values in a program.",
                   String.Join(Environment.NewLine, {
                       "1. UNDERSTANDING DATA HANDLING",
                       "- Data handling includes storing, using, and processing values in a program.",
                       "- Variables store values in memory, and their values can change while the program runs.",
                       "",
                       "2. VARIABLES AND DATA TYPES",
                       "- Each variable has a name and data type.",
                       "- String stores text and numbers not used in calculations.",
                       "- Integer and Short store whole numbers.",
                       "- Decimal and Single store fractional values.",
                       "- Choose a data type based on the kind of value and required precision.",
                       "",
                       "3. NAMING AND DECLARING VARIABLES",
                       "- Names can contain letters, digits, and underscores, and must start with a letter.",
                       "- Names cannot contain spaces, periods, or Visual Basic reserved words.",
                       "- Use Dim for local variables and Private for module-level variables.",
                       "- Names are not case-sensitive."
                   }),
                   New frmDataHandlingPractice(), True)
    End Sub

    Private Sub TheSelectionAndRepetitionStructureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TheSelectionAndRepetitionStructureToolStripMenuItem.Click
        ShowLesson("Lesson 6 - The Selection and Repetition Structure",
                   "Selection and repetition structures control program flow by making decisions based on conditions and repeating instructions through loops.",
                   String.Join(Environment.NewLine, {
                       "1. CONDITIONAL STATEMENTS",
                       "- If...Then runs statements when a condition is true.",
                       "- If...Then...Else handles true and false conditions. ElseIf checks additional conditions.",
                       "- Select Case compares one expression against possible values. Case Else handles unmatched values.",
                       "- Nested If statements place one decision inside another.",
                       "",
                       "2. RELATIONAL AND LOGICAL OPERATORS",
                       "- Relational operators compare values. A Boolean expression evaluates to True or False.",
                       "- And requires both conditions to be true. Or requires at least one to be true.",
                       "- Xor is true when only one condition is true. Not reverses a Boolean value.",
                       "- AndAlso and OrElse use short-circuit evaluation.",
                       "- Parentheses clarify evaluation order. A Boolean flag stores a True or False state used as a condition.",
                       "",
                       "3. STRING TESTING AND METHODS",
                       "- Strings can be compared using relational operators. String.Empty represents an empty string.",
                       "- ToUpper and ToLower change letter case in a returned string.",
                       "- IsNumeric checks whether a string contains numeric data. Length returns the number of characters.",
                       "- TrimStart, TrimEnd, and Trim remove spaces.",
                       "",
                       "4. REPETITION STRUCTURES",
                       "- A loop repeats statements. Each repetition is an iteration.",
                       "- Do While repeats while its condition is true. Do Until repeats until its condition becomes true.",
                       "- For...Next repeats over a specified range.",
                       "",
                       "5. NESTED LOOPS",
                       "- A nested loop places one loop inside another.",
                       "- Nested loops can process data arranged in rows and columns.",
                       "",
                       "NOTE",
                       "Date and time, Exit, and Continue are listed in the learning outcomes, but their details are not included in the readable lesson text."
                   }),
                   New frmSelectionRepetitionPractice(), True)
    End Sub

    Private Sub ARRAYSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ARRAYSToolStripMenuItem.Click
        ShowLesson("Lesson 7 - Arrays",
                   "An array stores multiple values of the same data type under one name. Its elements are accessed and processed by index, starting at zero.",
                   String.Join(Environment.NewLine, {
                       "1. ARRAY BASICS",
                       "- Each stored value is an element, accessed by its index or subscript.",
                       "- Array indexes begin at zero.",
                       "",
                       "2. DECLARING AND INITIALIZING ARRAYS",
                       "- An array declaration specifies its name, highest index, and data type.",
                       "- An array with highest index 6 has seven elements, indexed from 0 to 6.",
                       "- Elements receive default values based on their data type. Arrays can be initialized with values.",
                       "- A named constant can represent the highest index.",
                       "",
                       "3. ONE-DIMENSIONAL AND MULTIDIMENSIONAL ARRAYS",
                       "- A one-dimensional array stores one set of values.",
                       "- A two-dimensional array stores values in rows and columns and uses two indexes.",
                       "- Visual Basic supports arrays with more dimensions, though they are harder to manage.",
                       "",
                       "4. PROCESSING ARRAY DATA",
                       "- Loops can enter, display, sum, and average array elements.",
                       "- Nested loops can process rows and columns in a two-dimensional array.",
                       "- Procedures and functions can receive arrays as arguments. A function can return an array.",
                       "",
                       "5. FUNCTIONS AND ARRAYS",
                       "- A function has a name, optional parameters, and a return data type.",
                       "- Return sends a value back to the calling code.",
                       "- Functions can return numeric or nonnumeric values, including strings and Boolean values."
                   }),
                   New frmArraysPractice(), True)
    End Sub

    Private Sub WorkingWithControlsAndPropertiesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkingWithControlsAndPropertiesToolStripMenuItem.Click
        ShowLesson("Lesson 8 - Working with Controls and Properties",
                   "Windows Forms controls provide built-in dialog boxes for selecting colors and fonts, opening and saving files, and printing documents.",
                   String.Join(Environment.NewLine, {
                       "1. DIALOG BOXES IN WINDOWS FORMS",
                       "- Built-in dialog boxes help users open or save files, choose colors and fonts, and print documents.",
                       "- Dialog controls can be added from the Toolbox and appear in the component tray.",
                       "- ShowDialog displays a dialog and returns a DialogResult, such as OK, Cancel, Yes, or No.",
                       "",
                       "2. COMMON DIALOG CONTROLS",
                       "- ColorDialog lets users select or define a color.",
                       "- FontDialog lets users select a font and size. ShowColor displays the color option.",
                       "- OpenFileDialog lets users select a file to open. SaveFileDialog lets users choose a file name and save location.",
                       "- PrintDialog lets users select a printer and printing options.",
                       "",
                       "3. DIALOG PROPERTIES",
                       "- ColorDialog: Color, AllowFullOpen, and CustomColors.",
                       "- FontDialog: Font, Color, MinSize, MaxSize, and ShowColor.",
                       "- OpenFileDialog: FileName, Filter, InitialDirectory, and Multiselect.",
                       "- SaveFileDialog: FileName, Filter, DefaultExt, and OverwritePrompt.",
                       "",
                       "4. DIALOG METHODS AND EVENTS",
                       "- ShowDialog displays a dialog. Reset restores default settings.",
                       "- OpenFileDialog and SaveFileDialog provide file-opening methods.",
                       "- Some dialogs have events such as HelpRequest or Apply."
                   }),
                   Nothing, True, False)
    End Sub

    Private Sub DebuggingAndTracingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebuggingAndTracingToolStripMenuItem.Click
        ShowLesson("Week 10 - Debugging and Tracing", "", "", Nothing, True, False, True)
    End Sub

    Private Sub DotNetFrameworkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        ShowLesson("Week 11 - .NET Framework", "", "", Nothing, True, False, True)
    End Sub

    Private Sub MdiAndStringComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        ShowLesson("Week 12 - Multi Document Interface and String Comparison", "", "", Nothing, True, False, True)
    End Sub

    Private Sub DatabaseConnectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DatabaseConnectionToolStripMenuItem.Click
        ShowLesson("Week 13 - Database Concepts and VB.NET Database Connection", "", "", Nothing, True, False, True)
    End Sub

    Private Sub DataDrivenWeek14ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        ShowLesson("Week 14 - Developing Data-Driven Applications", "", "", Nothing, True, False, True)
    End Sub

    Private Sub DataDrivenWeek15ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DevelopingDataDrivenApplicationToolStripMenuItem.Click
        ShowLesson("Week 15 - Developing Data-Driven Applications: Updating Data Sources", "", "", Nothing, True, False, True)
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click

    End Sub
End Class
