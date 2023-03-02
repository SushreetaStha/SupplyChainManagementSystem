$(document).ready(function () {
    $("[href]").each(function () {
        let urlArr = window.location.href.split("/").slice(2, 4).join("/");
        urlArr = "http://" + urlArr;
        if (this.href == urlArr) {
            $(this).addClass("active");
        }

        if (this.href == "http://localhost:5179/Report/SalesReport" && urlArr.includes("Report")) {
            $(this).addClass("active");
        }
    });
});