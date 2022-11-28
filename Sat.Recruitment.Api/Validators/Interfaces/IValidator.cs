namespace Sat.Recruitment.Api.Validators.Interfaces
{
    public interface IValidator<Tvalidator, TObject>
    {
        bool IsValid(TObject obj, out string message);
    }
}
