import gulp from "gulp";
import filter from "gulp-filter";
import clean from "gulp-clean";
// import purgecss from "gulp-purgecss";
import uglify from "gulp-uglify";
import purify from "gulp-purify-css";
import replace from "gulp-replace";
import dom from "gulp-dom";


// const { series, parallel } = require("gulp");

gulp.task("optimizeCSS", () => {
    return gulp.src("./dist/preptm/browser/**")
        .pipe(
            filter([
                /* 
                glob pattern for CSS files, 
                point to files generated post angular prod build 
                */
                "**/styles-*.css"
            ])
        )

        // .pipe(purgecss({
        //     content: ["./dist/preptm/browser/**/*.js", "./dist/preptm/browser/**/*.html"]
        // }))

        .pipe(
            /* 
             glob pattern for JS files, to look for the styles usage
             the styles will be filtered based on the usage in the below files.
             Pointing to JS build output of Angular prod build
             */
            purify(["./dist/preptm/browser/**/*.js", "./dist/preptm/browser/**/*.html"], {
                info: true,
                minify: true,
                rejected: true,
                whitelist: []
            })
        )
        .pipe(replace(/\\;/gm, ';'))


        .pipe(gulp.dest("./dist/test/"));/* Optimized file output location */

})

gulp.task('optimizeJS', function () {
    return gulp.src('./dist/preptm/browser/**/*.js')
        .pipe(uglify())
        .pipe(gulp.dest('./dist/test/'));
});




/*
Delete style output of Angular prod build
*/
gulp.task("deleteCssJSFiles", () => {
    return gulp
        .src("./dist/preptm/browser/**")
        .pipe(filter(["**/styles-*.css", "**/*.js"]))
        .pipe(clean({ force: true }));
});


/*
Once the optimization & compression is done,
Replace the files in angular build output location
*/
gulp.task("copyNewCssJS", () => {
    return gulp.src("./dist/test/**/*").pipe(gulp.dest("./dist/preptm/browser"));
});

// #6 | Clear temp folder
gulp.task("clearTest", () => {
    return gulp
        .src("./dist/test/", { read: false })
        .pipe(clean({ force: true }));
});


// add defer in html files

gulp.task('deferJS', function () {
    return gulp.src('./dist/preptm/**/*.html')
        .pipe(dom(function () {
            this.querySelectorAll('script').forEach((script) => {
                script.setAttribute('async', '')
            })
            this.querySelectorAll('link').forEach((script) => {
                script.setAttribute('async', '')
            })
            return this;
        }))
        .pipe(gulp.dest('./dist/deferJS/'));
});

gulp.task("deleteDeferJSFilers", () => {
    return gulp
        .src("./dist/preptm/**/**")
        .pipe(filter(["**/*.html"]))
        .pipe(clean({ force: true }));
});

gulp.task("copyDeferJS", () => {
    return gulp.src("./dist/deferJS/**/*").pipe(gulp.dest("./dist/preptm"));
});

gulp.task("cleardeferJS", () => {
    return gulp
        .src("./dist/deferJS/", { read: false })
        .pipe(clean({ force: true }));
});

gulp.task("deleteENUSBroswer", () => {
    return gulp
        .src("./dist/preptm/browser/en-US/", { read: false })
        .pipe(clean({ force: true }));
});
gulp.task("deleteENUSServer", () => {
    return gulp
        .src("./dist/preptm/server/en-US/", { read: false })
        .pipe(clean({ force: true }));
});


export default gulp.series(
    "deleteENUSBroswer",
    "deleteENUSServer",
    "optimizeCSS",
    "optimizeJS",
    "deleteCssJSFiles",
    "copyNewCssJS",
    "clearTest",
    "deferJS",
    "deleteDeferJSFilers",
    "copyDeferJS",
    "cleardeferJS",
);
