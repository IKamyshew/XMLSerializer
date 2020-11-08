using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Enom
{
    public static class Pushlist
    {
        public static string RenderUpdatePushListHtml(XmlDocument response)
        {
            try
            {
                int errCount;
                List<string> errors = new List<string>();
                List<PushDomain> pushDomains = new List<PushDomain>();

                int.TryParse(response.SelectSingleNode("interface-response/ErrCount")?.InnerText, out errCount);

                if (errCount > 0)
                {
                    XmlNode xmlErrors = response.SelectSingleNode("interface-response/errors");
                    foreach (XmlNode xmlError in xmlErrors.ChildNodes)
                    {
                        errors.Add(xmlError.InnerText);
                    }
                }
                else
                {
                    XmlNodeList xmlPushDomains = response.SelectNodes("interface-response/UPDATEPUSHLIST/PUSH-DOMAIN");
                    foreach (XmlNode domain in xmlPushDomains)
                    {
                        var pushDomain = new PushDomain
                        {
                            DomainName = domain.SelectSingleNode("DomainName")?.InnerText,
                            SuccessfulPush = domain.SelectSingleNode("SuccessfulPush")?.InnerText,
                            Reason = domain.SelectSingleNode("Reason")?.InnerText,
                        };

                        pushDomains.Add(pushDomain);
                    }
                }

                return RenderUpdatePushListHtml(errCount > 0, errors, pushDomains);
            }
            catch
            {
                List<string> errors = new List<string>() { "Unexpected api response." };
                List<PushDomain> pushDomains = new List<PushDomain>();

                return RenderUpdatePushListHtml(errors.Count > 0, errors, pushDomains);
            }
        }

        private static String RenderUpdatePushListHtml(bool showErrorsSection, List<string> errors, List<PushDomain> pushDomains)
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<?xml version=\"1.0\" encoding=\"utf-16\"?>");

            if (showErrorsSection)
            {
                html.AppendLine("<div class=\"sError1\">One or more domains did not successfully push.");
                html.AppendLine("<br />");

                foreach (string error in errors)
                {
                    html.AppendLine(error);
                }

                html.AppendLine("<a href=\"/domains/pushlist.aspx\">Back</a>");
                html.AppendLine("</div>");
            }
            else
            {
                html.AppendLine("<div class=\"sAlert1\">Your push results are below</div>");
                html.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" class=\"sTbl\">");

                html.AppendLine("<thead>");
                html.AppendLine("<tr>");
                html.AppendLine("<th width=\"50%\">Domain Name</th>");
                html.AppendLine("<th>Success</th>");
                html.AppendLine("<th></th>");
                html.AppendLine("</tr>");
                html.AppendLine("</thead>");

                html.AppendLine("<tbody>");

                foreach (PushDomain domain in pushDomains)
                {
                    html.AppendLine("<tr>");

                    html.AppendLine("<td class=\"alt_1\">" + domain.DomainName + "</td>");
                    html.AppendLine("<td>" + domain.SuccessfulPush + "</td>");
                    html.AppendLine("<td>" + domain.Reason + "</td>");

                    html.AppendLine("</tr>");
                }

                html.AppendLine("</tbody>");
                html.AppendLine("</table>");
                html.AppendLine("<a href=\"/domains/pushlist.aspx\">Back</a>");
            }

            return html.ToString();
        }

        private class PushDomain
        {
            public string DomainName { get; set; }
            public string SuccessfulPush { get; set; }
            public string Reason { get; set; }
        }
    }
}
