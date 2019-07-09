using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.IO;

namespace Tests
{
    public class Tests
    {
        private BHHCSampleApi.Models.ReasonContext _context;
        private IEnumerable<BHHCSampleApi.Models.ReasonItem> _reasons;

        [SetUp]
        public async Task Setup()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"C:\Users\manoh\source\repos\NUnitTestProject1\NUnitTestProject2\appsettings.json")
                .Build();
            var connStr = config["ConnectionStrings:BHHCSampleApi"];
#if false
            //// Get the machine.config file.          
            //ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            //// You may want to map to your own exe.config file here.
            //fileMap.ExeConfigFilename = @"BHHCSampleApi.dll.config";
            //// You can add here LocalUserConfigFilename, MachineConfigFilename and RoamingUserConfigFilename, too
            //System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            ////var config = new Mock<IConfiguration>();
#endif
            var optionsBuilder = new DbContextOptionsBuilder<BHHCSampleApi.Models.ReasonContext>();
            optionsBuilder.UseSqlServer(connStr, providerOptions => providerOptions.EnableRetryOnFailure());
            _context = new BHHCSampleApi.Models.ReasonContext(optionsBuilder.Options);
            _reasons = (_context.ReasonItems as IEnumerable<BHHCSampleApi.Models.ReasonItem>);

        }

        [Test]
        public void GetReason13_ShouldReturnReason1()
        {
            var controller = new BHHCSampleApi.Controllers.ReasonController(_context);
            var reason = new BHHCSampleApi.Controllers.Reason(_context);
            var result = reason.GetReason(13);
            Assert.AreEqual(BHHCSampleApi.Models.ReasonContext.REASON1, result.Name);
        }
        [Test]
        public void GetReasonItem13_ShouldReturnReason1()
        {
            var controller = new BHHCSampleApi.Controllers.ReasonController(_context);
            var reason = new BHHCSampleApi.Controllers.Reason(_context);
            var result1 = controller.GetReasonItem(13);
            Assert.AreEqual(BHHCSampleApi.Models.ReasonContext.REASON1, result1.Result.Value.Name);
        }
        [Test]
        public async Task GetReasons_ShouldReturnAllReasons()
        {
            var testReasons = GetTestReasons();
            //var mock = new Mock<BHHCSampleApi.Controllers.IReason>();
            //mock.Setup(p => p.GetReason(13)).Returns(new BHHCSampleApi.Models.ReasonItem { Name = BHHCSampleApi.Models.ReasonContext.REASON1 });
            var controller = new BHHCSampleApi.Controllers.ReasonController(_context);

            var result = await controller.GetReasonItems();
            Assert.AreEqual(_reasons, result.Value);
        }
        private List<BHHCSampleApi.Models.ReasonItem> GetTestReasons()
        {
            var testReasons = new List<BHHCSampleApi.Models.ReasonItem>();
            testReasons.Add(new BHHCSampleApi.Models.ReasonItem { Id = 1, Name = "Reason1" });
            testReasons.Add(new BHHCSampleApi.Models.ReasonItem { Id = 2, Name = "Reason2" });
            testReasons.Add(new BHHCSampleApi.Models.ReasonItem { Id = 3, Name = "Reason3" });
            testReasons.Add(new BHHCSampleApi.Models.ReasonItem { Id = 4, Name = "Reason4" });

            return testReasons;
        }
    }
}