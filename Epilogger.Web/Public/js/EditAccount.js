head.ready(function() {

    var button = $('#uploadavator')[0];
    var uploader = new qq.FileUploader({
        element: button,
        allowedExtensions: ['jpg', 'jpeg', 'png', 'gif'],
        sizeLimit: 2147483647, // max size
        action: '/Account/UploadAvatar',
        multiple: false,
        uploadButtonText: 'upload an avatar',
        onComplete: function(id, fileName, responseJSON) {
            $("#theProfileImage").attr("src", responseJSON.imageurl);
            $("#ProfileImage").val(responseJSON.imageurl);
            $("#DisplayProfileImage").val(responseJSON.imageurl);
        }
    });
   

});