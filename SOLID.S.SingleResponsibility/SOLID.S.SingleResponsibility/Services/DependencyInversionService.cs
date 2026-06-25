namespace SOLID.Principles.Demo.Services
{
    public interface IReportGenerator
    {
        string Format { get; }
        string Generate(int requestDataCount);
    }


    public class PdfReportGenerator : IReportGenerator
    {
        public string Format => "Pdf";
        public string Generate(int requestDataCount) =>  $"data{requestDataCount}.pdf";
    }

    public class ExcelReportGenerator : IReportGenerator
    {
        public string Format => "Excel";
        public string Generate(int requestDataCount) => $"data{requestDataCount}.xlsx";
    }


    public interface IDependencyInversionService
    {
        string GenerateReport(string format, int requestDataCount);
    }



    public class DependencyInversionService: IDependencyInversionService 
    {
        private readonly IEnumerable<IReportGenerator> _generators;

        public DependencyInversionService(IEnumerable<IReportGenerator> generators)
        {
            _generators = generators;
        }

        public string GenerateReport(string format, int requestDataCount)
        {
            var generator = _generators.FirstOrDefault(g => g.Format == format);
            if (generator is null)
                return $"No generator registered for format: {format}";

            return generator.Generate(requestDataCount);
        }

    }
}
