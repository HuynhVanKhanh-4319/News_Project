

$(document).ready(function () {
    LoadNewsByCategory(1, "Thể Thao", "#sport_news_container", "Thể Thao");
    LoadNewsByCategory(1, "Tin trong nước", "#domestic_news_container", "Thời sự trong nước");
    
});
function EscapeHtml(text) {
    if (!text) return '';
    return text
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

function TruncateText(text, maxLength = 50) {
    if (!text) return '';
    return text.length > maxLength ? text.substring(0, maxLength) + '...' : text;
}

function RenderNewsCards(newsList) {
    if (!Array.isArray(newsList) || newsList.length === 0) {
        return `<div class="col-12"><p>Không có tin tức.</p></div>`;
    }

    let html = '';

    newsList.forEach(news => {
        const title = EscapeHtml(news.name);
        const titleShort = EscapeHtml(TruncateText(news.name, 50));
        const desc = news.description || '';
        const descShort = desc.length > 80 ? desc.substring(0, 80) + '...' : desc;
        const tooltipDesc = EscapeHtml(desc.replace(/(<([^>]+)>)/gi, ""));
        const publishedAt = news.publishedAt
            ? new Date(news.publishedAt).toLocaleString('vi-VN', {
                day: '2-digit', month: '2-digit', year: 'numeric',
                hour: '2-digit', minute: '2-digit'
            })
            : '';
        const imageUrl = 'https://file3.qdnd.vn/data/images/0/2023/12/02/upload_2081/img_4541.jpg?dpi=150&quality=100&w=870';

        html += `
            <div class="col-md-3 col-sm-6">
                <div class="card news-card mb-4">
                   <a href="/trang-tin-tuc/${news.metaUrl}">
                        <img src="${imageUrl}" class="card-img-top" alt="${title}" />
                    </a>
                    <div class="card-body">
                        <h5 class="card-title" data-bs-toggle="tooltip" title="${title}">
                            <a href="/trang-tin-tuc/${news.metaUrl}" class="text-decoration-none text-dark">
                                  ${titleShort}
                            </a>
                        </h5>
                        <p class="card-text" data-bs-toggle="tooltip" title="${tooltipDesc}">${descShort}</p>
                        <div class="news-meta">
                            <span><i class="far fa-clock"></i> ${publishedAt}</span>
                            <span class="ms-4"><i class="far fa-eye"></i> ${news.viewNumber || '0'}</span>
                        </div>
                    </div>
                </div>
            </div>
        `;
    });

    return html;
}

function LoadNewsByCategory(status = 1, categoryName, containerSelector, title) {
    $.ajax({
        url: `/News/GetList?status=${status}`,
        method: 'GET',
        success: function (response) {
            const newsList = response?.data || [];
            const filteredNews = newsList.filter(n =>
                (n.newsCategory?.name || n.newsCategoryObj?.name) === categoryName
            );

            const cardsHtml = RenderNewsCards(filteredNews);

            $(containerSelector).html(`
                <div class="row g-4 mt-5">
                    <div class="col-lg-12">
                        <div class="row g-4">
                            <div class="col-12">
                                <h3 class="mb-4 border-bottom pb-2">${title}</h3>
                            </div>
                            ${cardsHtml}
                        </div>
                    </div>
                </div>
            `);
            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
            tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl);
            });
        },
        error: function () {
            $(containerSelector).html('<p>Không thể tải dữ liệu tin tức.</p>');
        }
    });
}


