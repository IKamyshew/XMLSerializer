using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Enom.Test
{
    public class AccountSettingsTabsTest
    {
        ITestOutputHelper output;
        private AccountSettingsTabs tabControl { get; set; }
        private AccountSettingsTabsDto dto { get; set; }

        public AccountSettingsTabsTest(ITestOutputHelper output)
        {
            this.output = output;

            this.tabControl = new AccountSettingsTabs();

            // target dto to test
            this.dto = new AccountSettingsTabsDto
            {
                ActiveSubTab = TabType.Landrush,
                SiteType = SiteType.Enom,
                IsInEnomRetailGroup = true,
                IsNameOnly = false,
                IsTrafficOnly = false
            };
        }

        [Fact]
        public void TestRendering()
        {
            var viewModel = tabControl.MapDtoToViewModel(dto);

            output.WriteLine("ActiveSubTab: " + viewModel.ActiveSubTab.ToString());
            output.WriteLine("");
            foreach (var mainTab in viewModel.MainTabs)
            {
                output.WriteLine(
                    (mainTab.IsActive(viewModel.ActiveSubTab) ? "(Active) " : string.Empty)
                    + "Main Tab: Title - " + mainTab.Title
                    + " Href - " + mainTab.Href);

                foreach (var tab in mainTab.SubTabs)
                {
                    output.WriteLine("              "
                        + (tab.TabType == viewModel.ActiveSubTab ? "(Active) " : string.Empty)
                        + "Sub Tab: Title - " + tab.Title
                        + " Href - " + tab.Href);
                }

                output.WriteLine("");
            }
        }

        [Fact]
        public void EditContactTabs()
        {
            // arrange
            //var viewModel = new AccountSettingsTabsViewModel()
            //{
            //    ActiveSubTab = TabType.EditContact,
            //    MainTabs = new[] {
            //        new MainTab(new[] { new Tab(TabType.EditContact, "My Info", "/myaccount/editcontact.aspx") }.ToArray() ),
            //        new MainTab(new[] { new Tab(TabType.Defaults), new Tab(TabType.Dns) }.ToArray() ),
            //        new MainTab(new[] { new Tab(TabType.Balance) }.ToArray() ),
            //        new MainTab(new[] { new Tab(TabType.AccountValidationSetting) }.ToArray() ),
            //        new MainTab(new[] { new Tab(TabType.TwoFactorAuth) }.ToArray() )
            //    }
            //};

            //List<Tab> expectedMainTabs = new List<Tab>()
            //{
            //    new Tab(TabType.EditContact, "My Info", "/myaccount/editcontact.aspx", false),
            //    new Tab(TabType.Defaults, "Defaults", "/myaccount/Settings.asp?tab=default", false)
            //};

            //if (!dto.IsInEnomRetailGroup && dto.SiteType != SiteType.EnomCentral)
            //{
            //    expectedMainTabs.AddRange(new List<Tab>() {
            //        new Tab(TabType.Balance, "Reseller", "/myaccount/Settings.asp?tab=balance", false),
            //        new Tab(TabType.RenewalPricing, "Domain Pricing", "/myaccount/RenewalPricing.asp?tab=1", false),
            //        new Tab(TabType.QueuePricing, "Queue Pricing", "/myaccount/queuepricing.aspx", false),
            //        new Tab(TabType.ProductPricing, "Product Pricing", "/myaccount/ProductPricing.asp?tab=domainaddons", false),
            //    });
            //}

            //expectedMainTabs.AddRange(new List<Tab>() {
            //    new Tab(TabType.AccountValidationSetting, "Account Validation", "/account-security/account-validation-settings.aspx", false),
            //    new Tab(TabType.TwoFactorAuth, "Two Step Verification", "/two-factor-auth/two-factor-auth.aspx", false),
            //});

            //var expectedVM = new AccountSettingsTabsViewModel
            //{
            //    ActiveTab = TabType.EditContact,
            //    MainTabs = expectedMainTabs.ToArray()
            //};

            //this.DefaultTest(expectedVM, dto);
        }

        //private void DefaultTest(AccountSettingsTabsViewModel expectedVM, AccountSettingsTabsDto dto)
        //{
        //    // act
        //    var viewModel = tabControl.MapDtoToViewModel(dto);

        //    // assert
        //    Assert.NotNull(viewModel);
        //    Assert.Equal(viewModel.ActiveTab, expectedVM.ActiveTab);
        //    AssertTabsEqual(viewModel.MainTabs, expectedVM.MainTabs);
        //    AssertTabsEqual(viewModel.SubTabs, expectedVM.SubTabs);
        //}

        private void AssertTabsEqual(Tab[] renderedTabs, Tab[] expectedTabs)
        {
            Assert.Equal(renderedTabs.Length, expectedTabs.Length);

            for (int i = 0; i < expectedTabs.Length; i++)
            {
                var renderedTab = renderedTabs[i];
                var expectedTab = expectedTabs[i];

                Assert.Equal(renderedTab.Href, expectedTab.Href);
                Assert.Equal(renderedTab.Title, expectedTab.Title);
                Assert.Equal(renderedTab.TabType, expectedTab.TabType);
            }
        }
    }
}
