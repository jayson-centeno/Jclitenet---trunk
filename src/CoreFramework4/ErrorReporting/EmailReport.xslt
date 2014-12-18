<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <body>
        <italic>v1.001</italic>
        <h3>
          Error Information
        </h3>
        <hr />
        <br />
        <strong style="font-size: 1.2em">Request</strong>
        <table style="border: solid 1px #999; background-color: #eee; padding: 10px;">
          <tr>
            <td style="width: 18%">
              Date And Time
            </td>
            <td style="width: 2%">
              :
            </td>
            <td style="width: 80%">
              <xsl:value-of select="ErrorReport/Request/DateTime" />
            </td>
          </tr>
          <tr>
            <td>
              URL
            </td>
            <td>
              :
            </td>
            <td>
              <xsl:value-of select="ErrorReport/Request/URL" />
            </td>
          </tr>
          <tr>
            <td>
              Client IP
            </td>
            <td>
              :
            </td>
            <td>
              <xsl:value-of select="ErrorReport/Request/IPAddress" />
            </td>
          </tr>
          <tr>
            <td>
              Server
            </td>
            <td>
              :
            </td>
            <td>
              <xsl:value-of select="ErrorReport/Request/Server" />
            </td>
          </tr>
          <tr>
            <td>
              Physical Application Path
            </td>
            <td>
              :
            </td>
            <td>
              <xsl:value-of select="ErrorReport/Request/PhysicalApplicationPath" />
            </td>
          </tr>
          <tr>
            <td>
              Browser
            </td>
            <td>
              :
            </td>
            <td>
              <xsl:value-of select="ErrorReport/Request/Browser" />
            </td>
          </tr>
        </table>
        <hr />
        <br />
        <strong style="font-size: 1.2em">Session</strong>
        <table style="border: solid 1px #999; background-color: #eee; padding: 10px;">
          <xsl:for-each select="ErrorReport/Request/Session/Item">
            <tr>
              <td style="width: 18%">
                <xsl:value-of select="@Key" />
              </td>
              <td style="width: 2%">
                :
              </td>
              <td style="width: 80%">
                <xsl:value-of select="." />
              </td>
            </tr>
          </xsl:for-each>
        </table>
        <hr />
        <br />
        <strong style="font-size: 1.2em">Exceptions</strong>
        <xsl:for-each select="ErrorReport/Exception">
          <table>
            <tr>
              <td>
                Message
              </td>
              <td>
                :
              </td>
              <td>
                <xsl:value-of select="Message" />
              </td>
            </tr>
            <tr>
              <td>
                Method
              </td>
              <td>
                :
              </td>
              <td>
                <xsl:value-of select="Method" />
              </td>
            </tr>
            <tr>
              <td>
                Class
              </td>
              <td>
                :
              </td>
              <td>
                <xsl:value-of select="Class" />
              </td>
            </tr>
            <tr>
              <td>
                <b>Line</b>
              </td>
              <td>
                :
              </td>
              <td>
                <b>
                  <xsl:value-of select="Line" />
                </b>
              </td>
            </tr>
            <tr>
              <td>
                DLL
              </td>
              <td>
                :
              </td>
              <td>
                <xsl:value-of select="DLL" />
              </td>
            </tr>
            <tr>
              <td>
                Source
              </td>
              <td>
                :
              </td>
              <td>
                <xsl:value-of select="Source" />
              </td>
            </tr>
            <tr>
              <td>
                Stack Trace
              </td>
              <td>
                :
              </td>
              <td>
                <xsl:value-of select="StackTrace" />
              </td>
            </tr>
          </table>
          <xsl:for-each select="HttpException">
            <strong style="font-size: 1.2em">HTTP Exception</strong>
            <table style="border: solid 1px #999; background-color: #eee; padding: 10px;">
              <tr>
                <td style="width: 18%">
                  Date And Time
                </td>
                <td style="width: 2%">
                  :
                </td>
                <td style="width: 80%">
                  <xsl:value-of select="DateTime" />
                </td>
              </tr>
              <tr>
                <td>
                  Error Code
                </td>
                <td>
                  :
                </td>
                <td>
                  <xsl:value-of select="ErrorCode" />
                </td>
              </tr>
              <tr>
                <td>
                  Http Code
                </td>
                <td>
                  :
                </td>
                <td>
                  <xsl:value-of select="HttpCode" />
                </td>
              </tr>
              <tr>
                <td>
                  Html Error Message
                </td>
                <td>
                  :
                </td>
                <td>
                  <xsl:value-of select="HtmlErrorMessage" />
                </td>
              </tr>
            </table>
          </xsl:for-each>
          <hr />
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
