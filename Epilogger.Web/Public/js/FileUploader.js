head.ready(function() {

    var button = $('#uploadFile')[0];
    var uploader = new qq.FileUploader({
        element: button,
        allowedExtensions: ['jpg', 'jpeg', 'png', 'gif'],
        sizeLimit: 2147483647, // max size
        action: '/Event/UploadFile',
        multiple: true,
        uploadButtonText: 'upload',
        onComplete: function(id, fileName, responseJSON) {
            $("#theFileImage").attr("src", responseJSON.imageurl);
            $("#FileImage").val(responseJSON.imageurl);
            $("#DisplayFileImage").val(responseJSON.imageurl);
        }
    });
   

});