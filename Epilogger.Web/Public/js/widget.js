﻿var camera = document.getElementById("camera");
var bird = document.getElementById("bird");
var canvas = document.createElement("canvas");
var ctx = canvas.getContext("2d");
var originalPixels = null;
var currentPixels = null;

function getPixels(img) {
    canvas.width = img.width;
    canvas.height = img.height;

    ctx.drawImage(img, 0, 0, img.naturalWidth, img.naturalHeight, 0, 0, img.width, img.height);
    originalPixels = ctx.getImageData(0, 0, img.width, img.height);
    currentPixels = ctx.getImageData(0, 0, img.width, img.height);

    img.onload = null;
}

function HexToRGB(Hex) {
    var Long = parseInt(Hex.replace(/^#/, ""), 16);
    return {
        R: (Long >>> 16) & 0xff,
        G: (Long >>> 8) & 0xff,
        B: Long & 0xff
    };
}

function changeColor() {
    if (!originalPixels) return; // Check if image has loaded
    var newColor = HexToRGB(document.getElementById("color").value);

    for (var I = 0, L = originalPixels.data.length; I < L; I += 4) {
        if (currentPixels.data[I + 3] > 0) // If it's not a transparent pixel
        {
            currentPixels.data[I] = originalPixels.data[I] / 255 * newColor.R;
            currentPixels.data[I + 1] = originalPixels.data[I + 1] / 255 * newColor.G;
            currentPixels.data[I + 2] = originalPixels.data[I + 2] / 255 * newColor.B;
        }
    }

    ctx.putImageData(currentPixels, 0, 0);
    bird.src = canvas.toDataURL("image/png");
    camera.src = canvas.toDataURL("img/png");
}

head.ready(function () {
    changeColor();
});
