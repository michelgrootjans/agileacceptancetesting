using System;
using fit;
using Snacks_R_Us.Domain.DataTransfer;
using Snacks_R_Us.Domain.IoC;
using Snacks_R_Us.Domain.Services;

namespace Snacks_R_Us.AcceptanceTests
{
    public class CreateSnack : ColumnFixture
    {
        private readonly ISnackService snackService;

        public string Name { get; set; }
        public string Price { get; set; }
        public string Result { get; set; }

        public CreateSnack()
        {
            Init.FitNesseTests();

            snackService = Container.GetImplementationOf<ISnackService>();
        }

        public override void Execute()
        {
            try
            {
                snackService.CreateSnack(new CreateSnackDto{Name = Name, Price = Price});
                Result = "Success";
            }
            catch (Exception e)
            {
                Result = e.Message;
            }
        }
    }
}