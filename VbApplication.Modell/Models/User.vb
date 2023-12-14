Imports System
Imports System.Collections.Generic

Namespace Models
    Partial Public Class User
        Public Property Id As Long

        Public Property UserName As String

        Public Property Email As String

        Public Property Password As String

        Public Property Active As Boolean?

        Public Property CreatedDate As Date?

        Public Property UpdatedDate As Date?

        Public Property CustTitle As String

        Public Property CustGender As String

        Public Property CustDateOfBirth As Date?

        Public Property Department As String

        Public Property EmpPosition As String

        Public Property EmpSalary As Decimal?

        Public Property InternSchool As String

        Public Property InternGrade As String

        Public Property LegalpersonCompanyName As String

        Public Property LegalPersonTaxNumber As String

        Public Property LegalPersonAddress As String

        Public Property LegalPersonPhone As String

        Public Property LegalPersonFax As String

        Public Property SupContactName As String

        Public Property SupContactSurname As String

        Public Property SupContactTitle As String

        Public Overridable ReadOnly Property PasswordHistories As ICollection(Of PasswordHistory) = New List(Of PasswordHistory)()
    End Class
End Namespace
