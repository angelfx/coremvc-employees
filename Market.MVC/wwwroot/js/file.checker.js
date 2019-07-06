$(document).ready(function () {
    var $imgContainer = $('#imgContainer');

    $('#image').change(function () {
        $('#image').removeData('imageWidth');
        $('#image').removeData('imageHeight');
        $imgContainer.hide().empty();

        var file = this.files[0];
        var ext = file.name.substring(file.name.lastIndexOf('.') + 1);
        var extList = "jpg,png,JPG,PNG";
        extList.indexOf(ext);

        if (extList.indexOf(ext) >= 0) {
            var reader = new FileReader();

            reader.onload = function () {
                var $img = $('<img />').attr({ src: reader.result });

                $img.on('load', function () {
                    $imgContainer.append($img).show();
                    var imageWidth = $img.width();
                    var imageHeight = $img.height()
                    $('#image').data('imageWidth', imageWidth);
                    $('#image').data('imageHeight', imageWidth);
                    if (imageWidth != 200 || imageHeight != 200) {
                        $imgContainer.hide();
                    } else {
                        $img.css({ width: '200px', height: '200px' });
                    }
                });
            }
            reader.readAsDataURL(file);
        }
    });

    //Creating custom validate image
    //Image must be a jpg or png, size is lower than 500 kb, width and hegth are equals 200 px.
    jQuery.validator.addMethod("fileChecker", function (value, element) {
        if (document.getElementById("image").files.length === 0) {
            return true;
        } else {
            $('#alerts').hide();
            $('#alerts').empty();
            
            var file = document.getElementById("image").files[0];
            var files = document.getElementById("image").files;
            var parts = document.getElementById("image").value.split("\\");
            var filename = parts[parts.length - 1]; //Get file name
            var ext = file.name.substring(file.name.lastIndexOf('.') + 1); //Get extension
            $('#imageExt').val(ext);
            $('#imageName').val(filename);

            var errors = new Array();

            var extList = "jpg,png,JPG,PNG"; //allowed extensions
            var extArray = extList.split(",");

            //Validate file for size and extension
            //******
            if (file.size > 500 * 1024) {
                errors.push('Picture of greater than 500 Kb. Current size is ' + roundPlus(file.size / (1024), 2) + ' Kb<br/>');
            }

            var f = false;
            for (var j = 0; j < extArray.length; j++) {
                if (extArray[j] == ext.toLowerCase()) {
                    f = true;
                }
            }
            if (!f) {
                errors.push('Extension of file "' + ext + '" not allowed.<br/>');
            }

            var width = $('#image').data('imageWidth');
            var height = $('#image').data('imageHeight');

            if (width != 200 || height != 200)
                errors.push('Picture has dimensions different from 200х200.<br/>');

            if (errors.length > 0) {

                var errStr = '';
                for (var i = 0; i < errors.length; i++) {
                    errStr += '<div class="alert alert-warning" role="alert">' + errors[i] + '</div>';
                }

                $('#alerts').html(errStr);
                $('#alerts').show();
                return false;
            }

            if (files.length > 0) {
                $('#alerts').hide();
                return true;
            } else {
                alert("Your browser doesn't support HTML5!");
                return false;
            }

            //******

            
        }
    }, "* Picture must be lower than 500 Kb, width and height are equeals 200px. Image must be a jpg pr png");

    //Add custom validate to rules
    $('#image').rules("add", {
        fileChecker: true
    });

    //Search custom attribute. On edit page we can skip picture validation
    if ($('#image').attr("data-custom-required") === "true") {
        $('#image').rules("add", {
            required: true,
            messages: {
                required: "Input picture"
            }
        });
    }

});

function roundPlus(x, n) { //x - число, n - количество знаков
    if (isNaN(x) || isNaN(n)) return false;
    var m = Math.pow(10, n);
    return Math.round(x * m) / m;
    5
}
