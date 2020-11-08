using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enom
{
    public class AccountSettingsTabs
    {
        public AccountSettingsTabsViewModel ViewModel { get; set; }

        private const string MainTabActiveClass = "stabon";

        private const string SecondRowTabActiveClass = "sTab2SubOn";

        public AccountSettingsTabsViewModel MapDtoToViewModel(AccountSettingsTabsDto dtoModel)
        {
            if (dtoModel == null)
            {
                throw new ArgumentNullException("Dto Model for AccountSettingsTabs couldn't be null");
            }

            switch (dtoModel.SiteType)
            {
                case SiteType.Bulk:
                    return this.GetBulkRegisterTabsViewModel(dtoModel.ActiveSubTab);
                case SiteType.EnomCentral:
                    return this.GetEnomCentralTabsViewModel(dtoModel.ActiveSubTab);
                case SiteType.Enom:
                default:
                    if (dtoModel.IsNameOnly || dtoModel.IsTrafficOnly)
                    {
                        return new AccountSettingsTabsViewModel();
                    }

                    return this.GetEnomTabsViewModel(dtoModel.ActiveSubTab, dtoModel.RenewalSetting, dtoModel.IsInEnomRetailGroup);
            }
        }

        private AccountSettingsTabsViewModel GetBulkRegisterTabsViewModel(TabType activeSubTab)
        {
            var viewModel = new AccountSettingsTabsViewModel()
            {
                ActiveSubTab = activeSubTab,
                MainTabs = new[] {
                    new MainTab(new[] { new Tab(TabType.EditContact) }.ToArray() ),
                    new MainTab(new[] { new Tab(TabType.Defaults), new Tab(TabType.Dns) }.ToArray() ),
                    new MainTab(new[] { new Tab(TabType.Balance) }.ToArray() ),
                    new MainTab(new[] { new Tab(TabType.AccountValidationSetting) }.ToArray() ),
                    new MainTab(new[] { new Tab(TabType.TwoFactorAuth) }.ToArray() )
                }
            };

            return viewModel;
        }

        private AccountSettingsTabsViewModel GetEnomCentralTabsViewModel(TabType activeSubTab)
        {
            var viewModel = new AccountSettingsTabsViewModel()
            {
                ActiveSubTab = activeSubTab,
                MainTabs = new[] {
                    new MainTab(new[] { new Tab(TabType.EditContact) }.ToArray() ),
                    new MainTab(new[] { new Tab(TabType.Defaults), new Tab(TabType.Dns) }.ToArray() ),
                    new MainTab(new[] { new Tab(TabType.AccountValidationSetting) }.ToArray() ),
                    new MainTab(new[] { new Tab(TabType.TwoFactorAuth) }.ToArray() ),
                }
            };

            return viewModel;
        }

        private AccountSettingsTabsViewModel GetEnomTabsViewModel(TabType activeSubTab, int? renewalSetting, bool isInEnomRetailGroup)
        {
            var mainTabs = new List<MainTab> {
                new MainTab(new[] { new Tab(TabType.EditContact) }.ToArray() ),
                new MainTab(new[] { new Tab(TabType.Defaults), new Tab(TabType.Dns) }.ToArray() )
            };

            if (!isInEnomRetailGroup)
            {
                mainTabs.AddRange(new[] {
                    new MainTab(new[] {
                        new Tab(TabType.Balance),
                        new Tab(TabType.RenewalSettings, $"?RenewalSetting={renewalSetting}"),
                        new Tab(TabType.ConfirmationSettings)
                    }.ToArray()),
                    new MainTab(new[] {
                        new Tab(TabType.RenewalPricingOneYear),
                        new Tab(TabType.RenewalPricingTwoYear),
                        new Tab(TabType.RenewalPricingFiveYear),
                        new Tab(TabType.RenewalPricingTenYear),
                        new Tab(TabType.PremiumDomains)
                    }.ToArray()),
                    new MainTab(new[] { 
                        new Tab(TabType.Sunrise),
                        new Tab(TabType.Landrush),
                        new Tab(TabType.PreRegistration),
                    }.ToArray()),
                    new MainTab(new[] { 
                        new Tab(TabType.DomainAddons),
                        new Tab(TabType.LinuxHosting),
                        new Tab(TabType.WebHostingDiscounts),
                        new Tab(TabType.WebHosting) 
                    }.ToArray())
                });
            }

            mainTabs.AddRange(new[] {
                new MainTab(new[] { new Tab(TabType.AccountValidationSetting) }.ToArray()),
                new MainTab(new[] { new Tab(TabType.TwoFactorAuth) }.ToArray())
            });

            var viewModel = new AccountSettingsTabsViewModel()
            {
                ActiveSubTab = activeSubTab,
                MainTabs = mainTabs.ToArray()
            };

            return viewModel;
        }

        protected string RenderTabs(AccountSettingsTabsViewModel vm)
        {
            if (vm == null)
            {
                return null;
            }

            StringBuilder tabHtml = new StringBuilder();

            MainTab activeMainTab = null;
            if (vm.MainTabs.Count() > 0)
            {
                tabHtml.AppendLine($"<div class=\"sTab2\">");
                foreach (var tab in vm.MainTabs)
                {
                    string activeTabClass = null;
                    if (tab.IsActive(vm.ActiveSubTab))
                    {
                        activeMainTab = tab;
                        activeTabClass = $" class=\"{MainTabActiveClass}\"";
                    }

                    tabHtml.AppendLine($"<li{activeTabClass}>");
                    tabHtml.AppendLine($"<a href=\"{tab.Href}\">");
                    tabHtml.AppendLine($"<span>{tab.Title}</span>");
                    tabHtml.AppendLine("</a>");
                    tabHtml.AppendLine("</li>");
                }
                tabHtml.AppendLine("</div>");
            }

            // display subtabs only if at least 2 exist
            if (activeMainTab != null && activeMainTab.SubTabs.Count() > 1)
            {
                tabHtml.AppendLine($"<div class=\"sTab2Sub\">");
                for (int i = 0; i < activeMainTab.SubTabs.Count(); i++)
                {
                    var tab = activeMainTab.SubTabs[i];

                    string activeTabClass = null;

                    if (vm.ActiveSubTab == tab.TabType)
                    {
                        activeTabClass = $"class=\"{SecondRowTabActiveClass}\" ";
                    }

                    tabHtml.AppendLine($"<a {activeTabClass}href=\"{tab.Href}\">{tab.Title}</a>");

                    bool lastTab = i == (activeMainTab.SubTabs.Count() - 1);
                    if (!lastTab)
                    {
                        tabHtml.AppendLine($" | ");
                    }
                }

                tabHtml.AppendLine("</div>");
            }

            return tabHtml.ToString();
        }
    }

    public class AccountSettingsTabsDto
    {
        public TabType ActiveSubTab { get; set; }

        public SiteType SiteType { get; set; }

        public bool IsNameOnly { get; set; }

        public bool IsTrafficOnly { get; set; }

        public bool IsInEnomRetailGroup { get; set; }

        public int? RenewalSetting { get; set; }
    }

    public class AccountSettingsTabsViewModel
    {
        public TabType ActiveSubTab { get; set; } = TabType.None;

        public MainTab[] MainTabs { get; set; } = Array.Empty<MainTab>();
    }

    public enum SiteType
    {
        Enom = 1,
        EnomCentral = 2,
        Bulk = 3
    }

    public enum TabType
    {
        None = 0,

        EditContact,

        Defaults,
        Dns,

        Balance,
        RenewalSettings,
        ConfirmationSettings,

        Sunrise,
        Landrush,
        PreRegistration,

        RenewalPricingOneYear,
        RenewalPricingTwoYear,
        RenewalPricingFiveYear,
        RenewalPricingTenYear,
        PremiumDomains,

        WebHosting,
        LinuxHosting,
        WebHostingDiscounts,
        DomainAddons,

        AccountValidationSetting,
        TwoFactorAuth,
    }

    public class MainTab
    {
        public MainTab(Tab[] subTabs)
        {
            if (subTabs == null || subTabs.Length == 0)
                throw new ArgumentNullException($"Can't render main tab without at least one subTab.");

            this.SubTabs = subTabs;

            var firstSubTab = subTabs[0];
            this.Href = firstSubTab.Href;

            string title = null;
            switch (firstSubTab.TabType)
            {
                case TabType.EditContact: title = "My Info"; break;

                case TabType.Defaults:
                case TabType.Dns: title = "Defaults"; break;

                case TabType.Balance:
                case TabType.RenewalSettings:
                case TabType.ConfirmationSettings: title = "Reseller"; break;

                case TabType.AccountValidationSetting: title = "Account Validation"; break;
                case TabType.TwoFactorAuth: title = "Two Step Verification"; break;

                case TabType.RenewalPricingOneYear:
                case TabType.RenewalPricingTwoYear:
                case TabType.RenewalPricingFiveYear:
                case TabType.RenewalPricingTenYear:
                case TabType.PremiumDomains: title = "Domain Pricing"; break;

                case TabType.Sunrise:
                case TabType.Landrush:
                case TabType.PreRegistration: title = "Queue Pricing"; break;

                case TabType.DomainAddons:
                case TabType.LinuxHosting:
                case TabType.WebHostingDiscounts:
                case TabType.WebHosting: title = "Product Pricing"; break;
            }

            this.Title = title;
        }

        public string Title { get; set; }

        /// <summary>
        /// Gets the href. Always points to first subtab.
        /// </summary>
        public string Href { get; set; }

        public Tab[] SubTabs { get; set; } = Array.Empty<Tab>();

        /// <summary>
        /// Determines whether the specified main tab is active.
        /// </summary>
        /// <param name="activeSubTab">The active subtab.</param>
        /// <returns>True if there are any active subtabs.</returns>
        public bool IsActive(TabType activeSubTab) => SubTabs.Any(t => t.TabType == activeSubTab);
    }

    public class Tab
    {
        public Tab(TabType tabType, string query = null)
        {
            switch (tabType)
            {
                case TabType.EditContact:
                    this.InitTab(tabType, "My Info", "/myaccount/editcontact.aspx");
                    break;

                case TabType.Defaults:
                    this.InitTab(tabType, "Default Options", "/myaccount/settings.aspx?tab=default");
                    break;
                case TabType.Dns:
                    this.InitTab(tabType, "Default DNS", "/myaccount/settings.aspx?tab=dns");
                    break;

                case TabType.Balance:
                    this.InitTab(tabType, "Reseller Balance", "/myaccount/settings.aspx?tab=balance");
                    break;
                case TabType.RenewalSettings:
                    this.InitTab(tabType, "Reseller Renewal Settings", $"/myaccount/renewalsettings.aspx{query}");
                    break;
                case TabType.ConfirmationSettings:
                    this.InitTab(tabType, "Confirmation Email Settings", "/myaccount/confirmationsettings.aspx");
                    break;

                case TabType.Sunrise:
                    this.InitTab(tabType, "Sunrise", "/myaccount/queuepricing.aspx");
                    break;
                case TabType.Landrush:
                    this.InitTab(tabType, "Landrush", "/myaccount/queuepricing.aspx?queue=landrush");
                    break;
                case TabType.PreRegistration:
                    this.InitTab(tabType, "Pre-Registration", "/myaccount/queuepricing.aspx?queue=pre-register");
                    break;

                case TabType.RenewalPricingOneYear:
                    this.InitTab(tabType, "TLDs (1 Year)", "/myaccount/renewalpricing.aspx?tab=1");
                    break;
                case TabType.RenewalPricingTwoYear:
                    this.InitTab(tabType, "TLDs (2 Year)", "/myaccount/renewalpricing.aspx?tab=2");
                    break;
                case TabType.RenewalPricingFiveYear:
                    this.InitTab(tabType, "TLDs (5 Year)", "/myaccount/renewalpricing.aspx?tab=5");
                    break;
                case TabType.RenewalPricingTenYear:
                    this.InitTab(tabType, "TLDs (10 Year)", "/myaccount/renewalpricing.aspx?tab=10");
                    break;
                case TabType.PremiumDomains:
                    this.InitTab(tabType, "Premium Domains", "/myaccount/premium-domain-pricing.aspx");
                    break;

                case TabType.DomainAddons:
                    this.InitTab(tabType, "Web Services", "/myaccount/productpricing.aspx?tab=domainaddons");
                    break;
                case TabType.LinuxHosting:
                    this.InitTab(tabType, "Linux Hosting", "/myaccount/hostingpricing.aspx?tab=linuxhosting");
                    break;
                case TabType.WebHostingDiscounts:
                    this.InitTab(tabType, "Periodic Billing", "/myaccount/hostingtermdiscounts.aspx?tab=webhostingdiscounts");
                    break;
                case TabType.WebHosting:
                    this.InitTab(tabType, "Windows Hosting", "/myaccount/hostingpricing.aspx?tab=webhosting");
                    break;

                case TabType.AccountValidationSetting:
                    this.InitTab(tabType, "Account Validation", "/account-security/account-validation-settings.aspx");
                    break;
                case TabType.TwoFactorAuth:
                    this.InitTab(tabType, "Two Step Verification", "/two-factor-auth/two-factor-auth.aspx");
                    break;
            }
        }

        private void InitTab(TabType tabType, string title, string href)
        {
            if (tabType == TabType.None)
            {
                throw new ArgumentNullException($"Can't render tab with type None.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException($"Can't render tab without title");
            }

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new ArgumentNullException($"Can't render tab without href");
            }

            this.TabType = tabType;
            this.Title = title;
            this.Href = href;
        }

        public TabType TabType { get; set; }

        public string Title { get; set; }

        public string Href { get; set; }
    }
}
