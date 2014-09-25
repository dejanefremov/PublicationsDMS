var config = {
    //apiurl: "http://localhost:2319/",
    //apiurl: "http://localhost/PublicationsDMS.Web.Api/",
    apiurl: "http://localhost:8041/",
    //apiurl:  "http://publicationsdms.api.dejanefremov.com/",

    //weburl: "http://localhost:2329/",
    //weburl: "http://localhost/PublicationsDMS.Web/",
    weburl: "http://localhost:8051/",
    //weburl: "http://publicationsdms.dejanefremov.com/",

    generateApiUrl: function (serviceUrl) {
        return config.apiurl + serviceUrl;
    }
}