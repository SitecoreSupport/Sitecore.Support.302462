namespace Sitecore.Support.XA.Foundation.Multisite
{
  using Sitecore.Web;
  using Sitecore.XA.Foundation.Multisite;
  using System;
  using System.Linq;
  using System.Web;
  using System.Text.RegularExpressions;
  using System.Collections.Generic;

  public class SiteInfoResolver : Sitecore.XA.Foundation.Multisite.SiteInfoResolver
  {
    public override SiteInfo ResolveSiteFromRequest(SiteInfo[] possibleSites, HttpRequestBase request)
    {
      return ResolveSiteFromRequestImpl(possibleSites, request.Url.Host, request.Path);
    }
    private SiteInfo ResolveSiteFromRequestImpl(SiteInfo[] possibleSites, string hostName, string requestPath)
    {
      SiteInfo[] array = possibleSites.Where(delegate (SiteInfo s)
      {
        if (!(s.TargetHostName == hostName))
        {
          return s.HostName.Split('|').Contains(hostName);
        }
        return true;
      }).ToArray();
      if (array.Length == 1)
      {
        return array.First();
      }
      if (array.Length > 1)
      {
        SiteInfo siteInfo = ResolveByVirtualFolder(array, requestPath);
        if (siteInfo != null)
        {
          return siteInfo;
        }
      }
      List<SiteInfo> array3 = new List<SiteInfo>();
      foreach (SiteInfo s in possibleSites)
      {
        string[] subsites = s.HostName.Split('|');
        string regex;
        foreach (string n in subsites)
        {
          regex = n.Replace("*", ".*");
          if (!n.Equals("*") && !string.IsNullOrEmpty(n) && Regex.Match(hostName, regex).Success)
          {
            array3.Add(s);
          }
        }
      }
      SiteInfo[] array2 = array3.ToArray();
      if (array2.Length == 1)
      {
        return array2.First();
      }
      if (array2.Length > 1)
      {
        SiteInfo siteInfo2 = ResolveByVirtualFolder(array2, requestPath);
        if (siteInfo2 != null)
        {
          return siteInfo2;
        }
      }
      return null;
    }
  }
}
