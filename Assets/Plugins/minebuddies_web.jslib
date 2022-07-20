
    /*******************
     * 
     * 
     * https://forum.unity.com/threads/access-specific-files-in-idbfs.452168/#post-2931562
     *     var LibraryOpenPDF = {
        OpenPDFAtNewTab: function (base64)
        {
            var url = 'data:application/pdf;base64,' + Pointer_stringify(base64);
            window.open(url);
        },
    };
    mergeInto(LibraryManager.library, LibraryOpenPDF);

next, load the pdf data from C# and pass to plugins
Code (CSharp):

    var path = Application.persistentDataPath + "/" + fileNameWithFileType;
    var bytes = System.IO.File.ReadAllBytes(path);
    OpenPDF.AtNewTab(System.Convert.ToBase64String(bytes));

*******************/


var LibraryLinkToLog = {
    OpenLogFileInTab: function(base64) 
    {
        var url = 'data:text/csv;base64,' + Pointer_stringify(base64);
        window.open(url);    
    },
};

mergeInto(LibraryManager.library, LibraryLinkToLog);