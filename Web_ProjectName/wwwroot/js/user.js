document.addEventListener('DOMContentLoaded', function () {
    const breakingNews = document.querySelector('.breaking-news-content');
    let scrollAmount = 0;
    let scrollSpeed = 1; // pixels per frame

    function scrollBreakingNews() {
        if (breakingNews.scrollWidth > breakingNews.clientWidth) {
            scrollAmount += scrollSpeed;
            if (scrollAmount >= breakingNews.scrollWidth - breakingNews.clientWidth) {
                scrollAmount = 0;
            }
            breakingNews.scrollLeft = scrollAmount;
        }
        requestAnimationFrame(scrollBreakingNews);
    }

    requestAnimationFrame(scrollBreakingNews);

    // Pause on hover
    breakingNews.addEventListener('mouseenter', function () {
        scrollSpeed = 0;
    });

    breakingNews.addEventListener('mouseleave', function () {
        scrollSpeed = 1;
    });
});
