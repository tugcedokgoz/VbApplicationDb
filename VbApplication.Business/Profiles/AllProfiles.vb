Imports AutoMapper
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

Public Class AllProfiles
    Inherits Profile

    Public Sub New()
        CreateMap(Of User, UserGetDto)().ReverseMap()
        CreateMap(Of User, CustomerGetDto)().ReverseMap()
        CreateMap(Of User, EmployeeGetDto)().ReverseMap()
        CreateMap(Of User, InternGetDto)().ReverseMap()
        CreateMap(Of User, LegalPersonGetDto)().ReverseMap()
        CreateMap(Of User, SupplierGetDto)().ReverseMap()

        CreateMap(Of User, UserPostDto)().ReverseMap()
        CreateMap(Of User, CustomerPostDto)().ReverseMap()
        CreateMap(Of User, EmployeePostDto)().ReverseMap()
        CreateMap(Of User, InternPostDto)().ReverseMap()
        CreateMap(Of User, LegalPersonPostDto)().ReverseMap()
        CreateMap(Of User, SupplierPostDto)().ReverseMap()

        CreateMap(Of CustomerPostDto, UserPostDto)().ReverseMap()
        CreateMap(Of EmployeePostDto, UserPostDto)().ReverseMap()
    End Sub

End Class
