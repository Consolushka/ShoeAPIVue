"use strict";

var gulp = require("gulp");
var rename = require("gulp-rename");

var imagemin = require("gulp-imagemin");
var webp = require("gulp-webp");
var svgstore = require("gulp-svgstore");

gulp.task("images", function () {
  return gulp.src("src/assets/img/**/*.{png,jpg,svg}")
    .pipe(imagemin([
      imagemin.optipng({
        optimizationLevel: 3,
        progressive: true
      }),
      imagemin.svgo({
        plugins: [
          {
            removeViewBox: false
          }
        ]
      })
    ]))
    .pipe(gulp.dest("src/assets/img"));
});

gulp.task("webp", function () {
  return gulp.src("src/assets/img/*.{png,jpg}")
    .pipe(webp({quality: 80}))
    .pipe(gulp.dest("src/assets/img"));
});

gulp.task("svgstore", function () {
  return gulp.src("src/assets/img/icons/icon-*")
    .pipe(svgstore({
      inlineSvg: true
    }))
    .pipe(rename("sprite.svg"))
    .pipe(gulp.dest("src/assets/img"));
});

gulp.task("build", gulp.series(
  "images",
  "webp",
  "svgstore"
));
