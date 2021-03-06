<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>

<html>
    <head>  
        <title>PORFU-ERROR</title>

        <link type="image/x-icon" href="./resources/images/favicon.ico" rel="shortcut icon" />
        <link type="image/ico" href="./resources/images/favicon.ico" rel="icon" /> 
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
        <link rel="stylesheet" href="<c:url value="/resources/css/main.css" />"/>

    </head>
</html>

<div class="modal-content">

    <div class="modal-body">

        <div class="form-group">
            <label class="red" for="name">Error</label>
        </div>
        <span id="s"></span>
        <span id="i"></span>
    </div>

    <div class="modal-footer">
        <button id="errorButton" type="button" class="buttonNkbm" >Close</button>
    </div>

    <script>
        var i = document.getElementById('i');
        var s = document.getElementById('s');
        i.innerHTML = window.localStorage.getItem('error');
        s.innerHTML = window.localStorage.getItem('status');
        
        document.getElementById("errorButton").onclick = function () {
            window.localStorage.removeItem("error");
            location.href = "/PORFU_SpringTest/index.jsp";
        };

    </script>
</div>
