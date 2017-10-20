<%@page contentType="text/html"      %>
<%@page pageEncoding="UTF-8"         %>
<%@page isErrorPage='true'           %>
<html>
    <head> 
        <meta http-equiv="Content-Type"   content="text/html; charset=UTF-8">
        <meta http-equiv="Pragma"         content="no-cache">
        <meta http-equiv="Cache-Control"  content="no-cache">
        <meta http-equiv="expires"        content="Tue, 1 Jan 1980 01:00:00 GMT">
        <meta http-equiv="Refresh"        content="<%= "10; URL=" + bs.nkbm.util.WebUtils.getAppRoot(request)%>">
        <title>NAPAKA</title>
        <link href=<%=request.getContextPath()%>/resources/css/main.css rel="stylesheet" type="text/css"/>
    </head>
    <body>
        <table cellpadding="0" cellspacing="0" class="dummyIEstyle">
            <tbody>
                <tr>
                    <td>
                        <input type="image" src="<%=request.getContextPath()%>/images/blank.gif"  title="" />
                        <input type="hidden" name="autoScroll" />
                    </td>
                </tr>
            </tbody>
        </table>

        <table class="glavaTop" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td class="glavaTopColumn1"><input type="image" src="<%=request.getContextPath()%>/images/LogoA.jpg" title="Poslovni splet" /></td>
                    <td class="glavaTopColumn2"><span class="glavaAppName">NAPAKA</span></td>
                    <td class="glavaTopColumn3"><span></span></td>
                    <td class="glavaTopColumn3"><img src="<%=request.getContextPath()%>/images/nkbmLogo.jpg" alt="" /></td>
                </tr>
            </tbody>
        </table>
        <table class="glavaBottom" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td class="glavaBottomColumn1"><input type="image" src="<%=request.getContextPath()%>/images/LogoB.jpg" alt="" title="Poslovni splet" /></td>
                    <td class="glavaBottomColumn2"><img src="<%=request.getContextPath()%>/images/blank.gif" alt="" /></td>
                </tr>
            </tbody>
        </table>

        <table align="center" cellpadding="0" cellspacing="0" class="bodyMain" >
            <tbody>
                <tr>
                    <td class="bodyLeft">
                        <table cellpadding="0" cellspacing="0">
                            <tbody></tbody>
                        </table>
                    </td>
                    <td class="bodyRight">
                        <table cellpadding="0" cellspacing="0" class="content">
                            <tbody>
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="naslovBox">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <span class="errorMessage">
                                                            <%= bs.nkbm.util.DateUtils.dtmFormat_dd_MM_yyyy__HH_mm_ss.format(new java.util.Date())%> - Napaka v aplikaciji!<br/>
                                                        </span>
                                                        <span>
                                                            <%
                                                                if (request.getAttribute("javax.servlet.error.exception") != null) {
                                                                    String user = "N/A";
                                                                    String priimeNzv = " ";
                                                                    si.nkbm.auth.client.IUserInfo userInfo = null;
                                                                    if (request.getSession(true).getAttribute(si.nkbm.porfu.config.Config.APP_USER_IDENTIFIER) != null) {
                                                                        userInfo = (si.nkbm.auth.client.IUserInfo) session.getAttribute(si.nkbm.porfu.config.Config.APP_USER_IDENTIFIER);
                                                                        user = userInfo.getUser();
                                                                        priimeNzv = userInfo.getFirstName() + " " + userInfo.getLastName();
                                                                    }
                                                                    Exception ex = (Exception) request.getAttribute("javax.servlet.error.exception"); 
                                                                    try {

                                                                        bs.nkbm.util.WebUtils.sendWebErrorMail(ex, request, response, user, si.nkbm.porfu.config.Config.APP_MAIL_SERVER, si.nkbm.porfu.config.Config.getMailTo(), si.nkbm.porfu.config.Config.APP_MAIL_FROM, si.nkbm.porfu.config.Config.APP_MAIL_SUBJECT_ERROR + " - " + priimeNzv);
                                                                        out.println("<h2>Mail administratorju poslan.</h2>");

                                                                    } catch (Exception e) {
                                                                        e.printStackTrace();
                                                                        out.println("<font color='red'><h2>Mail ni bil uspesno poslan. Prosimo kontaktirajte administratorja.</h2></font>");
                                                                    }
                                                                }
                                                            %>                      
                                                        </span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>

        <table align="center" cellpadding="0" cellspacing="0" class="nogaTop">
            <tbody>
                <tr>
                    <td> </td>
                </tr>
            </tbody>
        </table>
        <table align="center" cellpadding="3" cellspacing="0" class="nogaBottom">
            <tbody>
                <tr>
                    <td><a href="http://www.nkbm.si" target="_blank"><span class="linkSmall">NKBM d.d. &copy; Copyright 2010 Nkbm d.d.</span></a></td>
                </tr>
            </tbody>
        </table>                  

    </body>
</html>
