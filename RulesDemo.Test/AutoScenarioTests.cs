using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using RulesDemo.Core.Services;
using RulesDemo.Core.Commands;
using RulesDemo.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RulesDemo.Test
{
    public class Tests
    {
        IServiceProvider provider;

        [OneTimeSetUp]
        public void Setup()
        {

            provider = new ServiceCollection()
                .AddTransient<AutoScenarioService>()
                .BuildServiceProvider();
        }

        [Test]
        public async Task SingleContainsIcd9RuleTest()
        {
            // arrange 
            var service = provider.GetService<AutoScenarioService>();
            var task = new ElrTaskList()
            {
                CdIcd9 = "11280"
            };

            var parameters = new List<AutoScenarioRuleParameter>()
            {
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "CdIcd9",
                    ValueCollection = new List<string>(){"11280"}
                }
            };

            var rule = new AutoScenarioRule()
            {
                AutoScenario = AutoScenarioType.CreateCase,
                Name = "ContainsIcd9",
                Parameters = parameters
            };

            var command = new AutoScenarioCommand()
            {
                Rule = rule,
                Task = task
            };

            // act 
            var autoScenario = await service.GetAutoScenario(command);

            // assert
            Assert.IsNotNull(autoScenario);
            Assert.IsTrue(autoScenario.Result == AutoScenarioType.CreateCase);
        }

        [Test]
        [TestCase("03841")]
        [TestCase("04830")]
        [TestCase("04823")]
        [TestCase("04800")]
        public async Task SeveralContainsIcd9RuleTest(string icd9Code)
        {
            // arrange 
            var service = provider.GetService<AutoScenarioService>();
            var task = new ElrTaskList()
            {
                CdIcd9 = icd9Code
            };

            var parameters = new List<AutoScenarioRuleParameter>()
            {
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "CdIcd9",
                    ValueCollection = new List<string>(){"03841","04830","04823","04800"}
                }
            };

            var rule = new AutoScenarioRule()
            {
                AutoScenario = AutoScenarioType.CreateCase,
                Name = "ContainsIcd9",
                Parameters = parameters
            };

            var command = new AutoScenarioCommand()
            {
                Rule = rule,
                Task = task
            };

            // act 
            var autoScenario = await service.GetAutoScenario(command);

            // assert
            Assert.IsNotNull(autoScenario);
            Assert.IsTrue(autoScenario.Result == AutoScenarioType.CreateCase);
        }

        [Test]
        [TestCase("03841")]
        [TestCase("04830")]
        [TestCase("04823")]
        [TestCase("00051")]
        public async Task Icd9ExcludeTest(string icd9Code)
        {
            // arrange 
            var service = provider.GetService<AutoScenarioService>();
            var task = new ElrTaskList()
            {
                CdIcd9 = icd9Code
            };

            var parameters = new List<AutoScenarioRuleParameter>()
            {
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "CdIcd9",
                    ValueCollection = new List<string>(){"00051"}
                }
            };

            var rule = new AutoScenarioRule()
            {
                AutoScenario = AutoScenarioType.CreateCase,
                Name = "ExcludesIcd9",
                Parameters = parameters
            };

            var command = new AutoScenarioCommand()
            {
                Rule = rule,
                Task = task
            };

            // act 
            var autoScenario = await service.GetAutoScenario(command);

            // assert
            Assert.IsNotNull(autoScenario);

            // Should fail, is in exclude list
            if (icd9Code == "00051")
            {
                Assert.IsTrue(autoScenario.Result == AutoScenarioType.Failure);
            }
            else
            {
                Assert.IsTrue(autoScenario.Result == AutoScenarioType.CreateCase);
            }
        }

        [Test]
        [TestCase("03841", "2020/01/01")]
        [TestCase("00241", "1994/06/05")]
        [TestCase("00421", "2011/05/03")]
        public async Task Icd9ContainsAndReportedDateEarlier(string icd9Code, DateTime reportedDate)
        {
            // arrange 
            var service = provider.GetService<AutoScenarioService>();
            var task = new ElrTaskList()
            {
                CdIcd9 = icd9Code,
                DtReported = reportedDate
            };

            var parameters = new List<AutoScenarioRuleParameter>()
            {
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "CdIcd9",
                    ValueCollection = new List<string>(){"03841","00241","00421"}
                },
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "DtReported",
                    CompareDate = new DateTime(2022,1,1)
                }
            };

            var rule = new AutoScenarioRule()
            {
                AutoScenario = AutoScenarioType.CreateCase,
                Name = "ContainsIcd9AndDateReportedEarlier",
                Parameters = parameters
            };

            var command = new AutoScenarioCommand()
            {
                Rule = rule,
                Task = task
            };

            // act 
            var autoScenario = await service.GetAutoScenario(command);

            // assert
            Assert.IsNotNull(autoScenario);
            Assert.IsTrue(autoScenario.Result == AutoScenarioType.CreateCase);
        }

        [Test]
        [TestCase("03841", "2022/07/20")]
        [TestCase("00821", "1984/03/14")]
        public async Task Icd9ContainsAndReportedDateEarlierFail(string icd9Code, DateTime reportedDate)
        {
            // arrange 
            var service = provider.GetService<AutoScenarioService>();
            var task = new ElrTaskList()
            {
                CdIcd9 = icd9Code,
                DtReported = reportedDate
            };

            var parameters = new List<AutoScenarioRuleParameter>()
            {
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "CdIcd9",
                    ValueCollection = new List<string>(){"03841","00241","00421"}
                },
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "DtReported",
                    CompareDate = new DateTime(2022,1,1)
                }
            };

            var rule = new AutoScenarioRule()
            {
                AutoScenario = AutoScenarioType.CreateCase,
                Name = "ContainsIcd9AndDateReportedEarlier",
                Parameters = parameters
            };

            var command = new AutoScenarioCommand()
            {
                Rule = rule,
                Task = task
            };

            // act 
            var autoScenario = await service.GetAutoScenario(command);

            // assert
            Assert.IsNotNull(autoScenario);
            Assert.IsTrue(autoScenario.Result == AutoScenarioType.Failure);
        }

        [Test]
        [TestCase("CRAYOLACERNER")]
        [TestCase("ZTESTN")]
        [TestCase("UNKNOWN")]
        [TestCase("TESTSALLY")]
        public async Task SpecialLastName(string lastName)
        {
            // arrange 
            var service = provider.GetService<AutoScenarioService>();
            var task = new ElrTaskList()
            {
                NmLast = lastName
            };

            var parameters = new List<AutoScenarioRuleParameter>()
            {
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "NmLast",
                    ValueCollection = new List<string>(){
                        "CRAYOLACERNER",
                        "SRC",
                        "LABTEST",
                        "LABQA",
                        "TESTING",
                        "CORRELATION",
                        "PRODTEST",
                        "PKTEST",
                        "ZTESTN",
                        "ZZTEST",
                        "ZZZTEST",
                        "ZZZLABTEST",
                        "TEST",
                        "PANEL",
                        "RACCOON",
                        "CONFIDENTIAL",
                        "COMPETENCY",
                        "MICRO QA",
                        "MICRO",
                        "MICROBIOLOGY",
                        "APINDMICRO",
                        "SURVEY",
                        "SURVEY-TPA",
                        "QA",
                        "TESTPATIENT",
                        "TESTINGCERNER",
                        "E CAP D-C",
                        "CAPSURVEY",
                        "PNJM",
                        "D-C 2018",
                        "YLAB",
                        "MERGED",
                        "SHIGELLA PANEL",
                        "CDC QA",
                        "D-C D- CAP",
                        "COVID",
                        "CAP-COV",
                        "DISCREPANCY",
                        "PKTEST",
                        "CAPLIS",
                        "INTERFACE",
                        "UPGRADE",
                        "UNKNOWN",
                        "SARS-COV-",
                        "ZZTESTMB",
                        "ZZTESTNCH",
                        "ZTESTD",
                        "CAPLIST",
                        "ZTEST",
                        "ZTESTS",
                        "ZTESTB",
                        "TQTEST",
                        "ZTESTT",
                        "COMP TEST",
                        "ITTESTING",
                        "TQTEST",
                        "WBLINDTEST",
                        "ZTESTA",
                        "ZTESTB",
                        "ZTESTC",
                        "ZTESTW",
                        "ZTESTDP",
                        "ZTESTVIP",
                        "ZZZTESTOP",
                        "ZLABBLIND",
                        "ZZDISASTER",
                        "ZZSURVEY",
                        "TRAUMA",
                        "VALIDATION",
                        "NON BILLABLE",
                        "LABORATORY",
                        "BDMAX",
                        "RESERVED",
                        "RESCUE",
                        "MEDICAL",
                        "NUMERICAL NAME",
                        "NUMERICAL AND ALPHABET COMBO",
                        "RESERVED",
                        "ANONYMOUS",
                        "TESTSALLY",
                        "PTESTCAPOVMC"
                    }
                }
            };

            var rule = new AutoScenarioRule()
            {
                AutoScenario = AutoScenarioType.CreateReport,
                Name = "ContainsSpecialLastName",
                Parameters = parameters
            };

            var command = new AutoScenarioCommand()
            {
                Rule = rule,
                Task = task
            };

            // act 
            var autoScenario = await service.GetAutoScenario(command);

            // assert
            Assert.IsNotNull(autoScenario);
            Assert.IsTrue(autoScenario.Result == AutoScenarioType.CreateReport);
        }

        [Test]
        [TestCase("04823", "INCOMPLETE", 7, "CONFIRMED", "AUTO_CR")]
        [TestCase("04830", "INCOMPLETE", 8, "CONFIRMED", "AUTO_CR")]
        [TestCase("04830", "2016COMP", 9, "CONFIRMED", "AUTO_CR")]
        [TestCase("04830", "2016COMP", 10, "CONFIRMED", "CHRONIC")]
        [TestCase("04830", "2016COMP", 11, "CONFIRMED", "AUTO_IMP")]
        [TestCase("04830", "2016COMP", 12, "CONFIRMED", "BG_SALM")]
        [TestCase("04830", "2016COMP", 13, "CONFIRMED", "BG_CHRONIC")]
        public async Task ElrCaseHandlingTest(string icd9Code, string statusCode, short age, string dxStatusCode, string idAdded)
        {
            // arrange 
            var service = provider.GetService<AutoScenarioService>();
            var task = new ElrTaskList()
            {
               EpiCase = new EpiCase()
               {
                   CdIcd9 = icd9Code,
                   CdStatus = statusCode,
                   AmAge = age,
                   CdDxStatus = dxStatusCode,
                   IdAdded = idAdded,
               }
            };

            var parameters = new List<AutoScenarioRuleParameter>()
            {
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "CdIcd9",
                    ValueCollection = new List<string>(){"04823","04830"}
                },
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "CdStatus",
                    ValueCollection = new List<string>(){"INCOMPLETE","2016COMP"}
                },
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "AmAge",
                    RangeLow = 6,
                    RangeHigh = 999
                },
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "CdDxStatus",
                    ValueCollection = new List<string>(){"CONFIRMED"}
                },
                new AutoScenarioRuleParameter()
                {
                    ColumnName = "IdAdded",
                    ValueCollection = new List<string>(){"AUTO_CR", "CHRONIC", "AUTO_IMP", "BG_SALM", "BG_CHRONIC"}
                }
            };

            var rule = new AutoScenarioRule()
            {
                AutoScenario = AutoScenarioType.AutoReport,
                Name = "ElrEpiCaseRule",
                Parameters = parameters
            };

            var command = new AutoScenarioCommand()
            {
                Rule = rule,
                Task = task
            };

            // act 
            var autoScenario = await service.GetAutoScenario(command);

            // assert
            Assert.IsNotNull(autoScenario);
            Assert.IsTrue(autoScenario.Result == AutoScenarioType.AutoReport);
        }
    }
}