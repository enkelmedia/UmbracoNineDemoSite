﻿using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Dictionary;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Templates;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;
using UmbracoNineDemoSite.Core.Features.Shared.Constants;
using UmbracoNineDemoSite.Core.Features.Shared.Settings;
using UmbracoNineDemoSite.Tests.Extensions;
using gM = UmbracoNineDemoSite.Core;

namespace UmbracoNineDemoSite.Tests.Unit.Features.Shared.Settings
{
    [TestFixture]
    public class SiteSettingsTests
    {
        private Mock<ICultureDictionaryFactory> cultureDictionaryFactory;
        private Mock<IUmbracoComponentRenderer> componentRenderer;
        private Mock<IPublishedContentQuery> publishedContentQuery;

        private Mock<UmbracoHelper> umbracoHelper;
        private Mock<IPublishedContent> siteSettings;
        private Mock<IPublishedContent> rootNode;
        private SiteSettings siteSettingsViewModel;

        [SetUp]
        public void SetUp()
        {
            siteSettings = new Mock<IPublishedContent>();           
            //siteSettings.SetupPropertyValue(nameof(gM.SiteSettings.FooterText), "footerText");
            rootNode = new Mock<IPublishedContent>();
            rootNode.Setup(query => query.Children).Returns(new IPublishedContent[] { siteSettings.Object });
            rootNode.SetupPropertyValue(nameof(gM.Home.Name), "homeName");
            this.cultureDictionaryFactory = new Mock<ICultureDictionaryFactory>();
            this.componentRenderer = new Mock<IUmbracoComponentRenderer>();
            this.publishedContentQuery = new Mock<IPublishedContentQuery>();
            publishedContentQuery.Setup(q => q.ContentAtRoot()).Returns(new List<IPublishedContent>() { rootNode.Object });


            this.umbracoHelper = new Mock<UmbracoHelper>(this.cultureDictionaryFactory.Object, this.componentRenderer.Object, this.publishedContentQuery.Object);
        }
        /*
        [Test]
        [TestCase("Site name")]
        [TestCase("Umbraco 9 Demo")]
        public void Given_HomeNodeExistsAtXPath_When_GetSiteName_Then_ReturnExpectedSiteNameFromHomeNode(string siteName)
        {
            var homeNode = new Mock<IPublishedContent>();
            homeNode.Setup(home => home.Name).Returns(siteName);
            this.MockContentQuerySiteSettings(homeNode.Object);

            var result = this.siteSettings.SiteName;

            Assert.AreEqual(siteName, result);
        }

        [Test]
        [TestCase("Call To Action Header")]
        [TestCase("Welcome to the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionHeader_When_GetCallToActionHeader_Then_ReturnExpectedCallToActionHeader(string callToActionHeader)
        {
            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(PropertyAlias.CallToActionHeader, callToActionHeader);
            //this.MockContentQueryXPAth($"//{ContentTypeAlias.SiteSettings}", settingsNode.Object);

            var result = this.siteSettings.CallToActionHeader;

            Assert.AreEqual(callToActionHeader, result);
        }

        [Test]
        [TestCase("Call To Action Description")]
        [TestCase("Description for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionDescription_When_GetCallToActionDescription_Then_ReturnExpectedCallToActionDescription(string callToActionDescription)
        {
            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(PropertyAlias.CallToActionDescription, callToActionDescription);
            this.MockContentQuerySiteSettings(settingsNode.Object);

            var result = this.siteSettings.CallToActionDescription;

            Assert.AreEqual(callToActionDescription, result);
        }

        [Test]
        public void Given_SettingsNodeHasCallToActionUrl_When_GetCallToActionUrl_Then_ReturnExpectedCallToActionUrl()
        {
            var callToActionContentReference = Mock.Of<IPublishedContent>();

            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(nameof(gM.SiteSettings.CallToActionUrl).ToCamelCase(), callToActionContentReference);
            this.MockContentQuerySiteSettings(settingsNode.Object);

            var result = this.siteSettings.CallToActionUrl;

            Assert.AreEqual(callToActionContentReference, result);
        }

        [Test]
        [TestCase("Call To Action Label")]
        [TestCase("Label for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasCallToActionButtonLabel_When_GetCallToActionButtonLabel_Then_ReturnExpectedCallToActionButtonLabel(string callToActionButtonLabel)
        {
            var settingsNode = new Mock<IPublishedContent>();
            settingsNode.SetupPropertyValue(PropertyAlias.CallToActionButtonLabel, callToActionButtonLabel);
            this.MockContentQuerySiteSettings(settingsNode.Object);

            var result = this.siteSettings.CallToActionButtonLabel;

            Assert.AreEqual(callToActionButtonLabel, result);
        }
        */
        [Test]
        [TestCase("Footer Text")]
        [TestCase("Footer Text for the Umbraco 9 Demo")]
        public void Given_SettingsNodeHasFooterText_When_GetFooterText_Then_ReturnExpectedFooterText(string footerText)
        {
            siteSettings.SetupPropertyValue(nameof(gM.SiteSettings.FooterText), footerText);
            siteSettingsViewModel = new SiteSettings(umbracoHelper.Object);
            var result = this.siteSettingsViewModel.FooterText;

            Assert.AreEqual(footerText, result);
        }

        private void MockContentQuerySiteSettings(IPublishedContentQuery query)
        {
            this.publishedContentQuery.Setup(query => query.ContentAtRoot()).Returns(new List<IPublishedContent>() { query.Content(1) });
        }
    }
}
