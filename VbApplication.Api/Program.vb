Imports AutoMapper
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports System
Imports VbApplication.Business
Imports VbApplication.DataAccess
Imports VbApplication.DataAccess.Models

Module Program
    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)

        ' Konteynere servis ekle.
        builder.Services.AddControllers()
        ' Swagger/OpenAPI yapýlandýrmasý hakkýnda daha fazla bilgi için: https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()


        builder.Services.AddScoped(Of IUserRepository, UserRepository)
        builder.Services.AddScoped(Of IPasswordHistoryRepository, PasswordHistoryRepository)

        builder.Services.AddScoped(Of IUserBs, UserBs)
        builder.Services.AddScoped(Of ICustomerBs, CustomerBs)
        builder.Services.AddScoped(Of IEmployeeBs, EmployeeBs)
        builder.Services.AddScoped(Of IInternBs, InternBs)
        builder.Services.AddScoped(Of ILegalPersonBs, LegalPersonBs)
        builder.Services.AddScoped(Of ISupplierBs, SupplierBs)

        builder.Services.AddAutoMapper(GetType(AllProfiles))
        builder.Services.AddDbContext(Of VbApplicationDbContext)()

        Dim app = builder.Build()

        ' HTTP istek pipeline'ýný yapýlandýr.
        If app.Environment.IsDevelopment() Then
            app.UseSwagger()
            app.UseSwaggerUI()
        End If
        app.UseHttpsRedirection()
        app.UseAuthorization()
        app.MapControllers()

        app.Run()
    End Sub
End Module