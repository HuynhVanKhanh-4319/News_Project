const RANGE_PAGE_DISPLAY = 5;
let $keywordEl = $("#input_keyword"),
    $cEl = $("#input_category"),
    $typeEl = $("#select_type_search"),
    $recordEl = $("#input_record"),
    $pageEl = $("#input_page");
let $productCategorySearchEl = $('.input_checkbox_product_category_search');
let $countRecordsEl = $(".count_record");
const ERROR_MESSAGE_HTML = `<div class="project item col-12 text-center p-2">
                                <h4>Kết nối không ổn định!</h4>
                                <button type="button" class="btn btn-primary rounded-pill"
                                    onclick="LoadListMainData();$(this).parent().remove();">Tải lại
                                </button>
                            </div>`;
const LIST_COLORS = ["red", "green", "yellow", "purple", "orange", "pink"];
const DEFAULT_TYPE = 1;

//Data param
const dataParms = function () {
    return {
        keyword: HtmlEncode($keywordEl.val()),
        type: $typeEl.val() ?? DEFAULT_TYPE,
        c: $cEl.val() ?? 0,
        record: $recordEl.val(),
        page: $pageEl.val()
    }
}

$(document).ready(function () {

    //Handling filter UI when load page
    HandlingUrlToMapUI();

    //Load data
    LoadListMainData();

    //On submit form search main
    $('#form_search_main').on('submit', function (e) {
        e.preventDefault();
        let value = $keywordEl.val();
        $pageEl.val(1);
        let record = $recordEl.val();

        //Change Link
        let newHref = `${GetLink(1, "keyword=", value)}record=${record}&page=1`;
        ChangeURLWithOut("", newHref); //not refresh page
        LoadListMainData();
    });

    //On checked product category
    $productCategorySearchEl.change(function () {
        OnchangeProductCategory();
    });

    //On change type
    $typeEl.change(function () {
        let data = $typeEl.val();
        $pageEl.val(1);
        let record = $recordEl.val();

        //Change Link
        let newHref = `${GetLink(1, "type=", data)}record=${record}&page=1`;
        ChangeURLWithOut("", newHref); //not refresh page

        //Get data
        LoadListMainData();
    });

});

//Load list
function LoadListMainData() {
    let data = dataParms();
    ShowOverlay("#div_main_data");
    $.ajax({
        type: 'GET',
        url: '/Product/GetList',
        data: data,
        dataType: "json",
        success: function (response) {
            HideOverlay("#div_main_data");
            //Check Error code
            if (response.result !== 1) {
                $("#div_main_data").html(ERROR_MESSAGE_HTML);
                theme.isotope();
                return;
            }

            let listData = response.data;
            let tmpHtml = '';
            let priceHtml = '';
            let ratioDiscountHtml = '';
            if (listData != null && listData.length > 0) {
                $.each(listData, function (key, value) {
                    let tmpRatio = CalDiscountPriceRatio(value.discount, value.price);
                    ratioDiscountHtml = '';
                    if (tmpRatio > 0)
                        ratioDiscountHtml = `<span class="avatar bg-theme-second text-white w-10 h-10 position-absolute text-uppercase fs-13" style="top: 1rem; left: 1rem;"><span>-${tmpRatio < 10 ? "0" + tmpRatio : tmpRatio}%</span></span>`;
                    if (value.discount > 0) {
                        priceHtml = `<ins><span class="amount">${NumberWithCommas(value.price, ',')}đ</span><ins> `;
                        priceHtml += `<del><span class="amount">${NumberWithCommas(value.discount, ',')}đ</span></del>`;
                    } else if (value.price > 0) {
                        priceHtml = `<span class="amount">${NumberWithCommas(value.price, ',')}đ</span>`;
                    } else {
                        priceHtml = `<span class="amount"></span>`;
                    }
                    let randomIndex = Math.floor(Math.random() * LIST_COLORS.length);
                    tmpHtml +=
                        `<div class="project item col-md-6 col-xl-4">
                            <div class="position-relative h-100">
                                <div class="shape rounded bg-soft-${LIST_COLORS[randomIndex]} rellax d-md-block" data-rellax-speed="0" style="bottom: -0.75rem; right: -0.75rem; width: 98%; height: 98%; z-index:0"></div>
                                <div class="card h-100">
                                    <figure class="lift rounded spotlight"><a href="/san-pham/${value.metaUrl}"><img class="img-fluid img_product_list" loading="lazy" src="${value.imageObj?.mediumUrl ?? '/images/error-image.png'}" srcset="${value.imageObj?.smallUrl ?? '/images/error-image.png'} 2x" alt="${value.name ?? ''}"></a></figure>
                                    <div class="card-body px-3 py-3">
                                        <div class="d-flex flex-row align-items-center justify-content-center mb-2">
                                            <div class="post-category text-ash mb-0 sp-line-1 fs-14" title="${value.productCategoryId > 0 ? (value.productCategoryObj?.name ?? '') : ''}">${value.productCategoryId > 0 ? (value.productCategoryObj?.name ?? '') : ''}</div>
                                        </div>
                                        <h2 style="height:50px;" class="post-title text-center h3 fs-18 sp-line-2" data-toggle="tooltip" data-placement="top" title="${value.name ?? ''}"><a href="/san-pham/${value.metaUrl}" class="link-dark">${value.name ?? ''}</a></h2>
                                        <p class="price">${priceHtml}</p>
                                    </div>
                                </div>
                            </div>
                        </div>`;
                });
                $("#div_main_data").html(tmpHtml);
                $('[data-toggle="tooltip"]').tooltip();
                if (IsNullOrEmty(data.keyword))
                    $countRecordsEl.html(`${response.data2nd != null ? response.data2nd : 0} kết quả`);
                else
                    $countRecordsEl.html(`<span title="${data.keyword}">'${data.keyword.length > 50 ? data.keyword.substring(0, 50) + "..." : data.keyword}'</span>: ${response.data2nd != null ? response.data2nd : 0} kết quả`);
            }
            else {
                if (IsNullOrEmty(data.keyword))
                    $countRecordsEl.html(`${response.data2nd != null ? response.data2nd : 0} kết quả`);
                else
                    $countRecordsEl.html(`<span title="${data.keyword}">'${data.keyword.length > 50 ? data.keyword.substring(0, 50) + "..." : data.keyword}'</span>: ${response.data2nd != null ? response.data2nd : 0} kết quả`);
                $("#div_main_data").html(`<div class="project item col-12 text-center p-2">${_imageErrorUrl.notFound}</div>`);
            }
            let totalRecord = parseInt(response.data2nd);
            let pageSize = parseInt(data.record);
            let currentPage = parseInt(data.page);
            LoadPagination(totalRecord, pageSize, currentPage);
            theme.isotope();
        },
        error: function (error) {
            HideOverlay("#div_main_data");
            $("#div_main_data").html(ERROR_MESSAGE_HTML);
            theme.isotope();
            console.log("Error when load product!");
        }
    });
}

//Caculate ratio discount price
function CalDiscountPriceRatio(num1, num2) {
    if (num1 == null || num1 === 0) return 0;
    let result = parseInt((num2 - num1) * 100 / num2);
    return result > 0 ? result : 1;
}

//Get value ProductCategory
function GetProductCategoryData() {
    let listChecked = "";
    $productCategorySearchEl.each(function (item, index) {
        if ($(this).is(":checked"))
            listChecked += $(this).val() + ",";
    });
    if (listChecked != "")
        listChecked = listChecked.slice(0, -1);
    return listChecked;
}

//Onchange ProductCategory
function OnchangeProductCategory() {
    let data = GetProductCategoryData();
    $pageEl.val(1);
    $cEl.val(data);
    let record = $recordEl.val();

    //Change Link
    let newHref = `${GetLink(1, "c=", data)}record=${record}&page=1`;
    ChangeURLWithOut("", newHref); //not refresh page

    //Get data
    LoadListMainData();
}

//Get link url
function GetLink(type, name, value) {
    let str = window.location.search.substring(1);
    let res = str.split("&");
    let test = res.splice(-2, 2);
    let link = "";
    if (test[0].indexOf("record") > -1 && test[1].indexOf("page") > -1) {
        res.forEach(function (item, index) {
            link += item + "&";
        });
    }
    else {
        if (str == "") {
            link = str;
        }
        else {
            link = str + "&";
        }
    }
    res = link.split("&");
    //Handling add edit remove param
    if (type == 1) {//News value
        let findItem = FindItemByKeywordInArrayString(res, name);
        if (value == "") { //remove
            link = link.replace(`${findItem}&`, '');
        } else {
            if (findItem == "") { //add
                link += `${name}${value}&`;
            } else {
                link = link.replace(`${findItem}&`, `${name}${value}&`);
            }
        }
    }
    return window.location.pathname + "?" + link;
}

//Handling url map UI dom
function HandlingUrlToMapUI() {
    let urlString = window.location.search.substring(1);
    let arr = urlString.split("&");
    let c, keyword, type, page, record;
    c = FindItemByKeywordInArrayString(arr, "c=").split("=")[1];
    keyword = FindItemByKeywordInArrayString(arr, "keyword=").split("=")[1];
    type = FindItemByKeywordInArrayString(arr, "type=").split("=")[1];
    page = FindItemByKeywordInArrayString(arr, "page=").split("=")[1];
    record = FindItemByKeywordInArrayString(arr, "record=").split("=")[1];

    $typeEl.val(type ?? DEFAULT_TYPE);
    $pageEl.val(page ?? 1);
    $recordEl.val(record ?? 24);

    //Product category
    try {
        if (c != "") {
            let listChecked = c.split(",");
            $productCategorySearchEl.each(function (item, index) {
                if (listChecked.indexOf($(this).val()) > -1)
                    $(this).prop('checked', true);
            });
        }
        else { //Remove checked
            $productCategorySearchEl.prop('checked', false);
        }
    } catch (e) {
        $productCategorySearchEl.prop('checked', false);
    }
}

//Click pagination changepage
function ChangePage(page, e, elm) {
    e.preventDefault();
    ScrollToTop('#div_main_data', 200, 500);
    $pageEl.val(page);

    //Change Link
    let newHref = $(elm).attr("href");
    ChangeURLWithOut("", newHref);

    //Get data
    LoadListMainData();
}

//Handle pagination
function LoadPagination(totalRecords, pageSize = 12, currentPage = 1) {
    let totalPage = Math.ceil(totalRecords / pageSize);
    //Check currentPage error
    if (currentPage > totalPage) {
        currentPage = totalPage;
    }
    if (currentPage < 1) {
        currentPage = 1;
    }

    let startPage = parseInt(Math.max(1, Math.ceil(currentPage - RANGE_PAGE_DISPLAY / 2)));
    let endPage = parseInt(Math.min(totalPage, currentPage + RANGE_PAGE_DISPLAY / 2));

    let html = '';
    if (totalPage > 1) {
        let link = GetLink();
        if (currentPage > 1)
            html += `
                <li>
                    <a class="page-link" href="${link}record=${pageSize}&page=${currentPage - 1}" title="Trang trước" onclick="ChangePage(${currentPage - 1},event,this)" aria-label="Previous">
                        <i class="uil uil-arrow-left"></i>
                    </a>
                </li>`;

        for (let i = startPage; i <= endPage; i++)
            if (currentPage == i)
                html += `<li class="page-item active"><a class="page-link">${currentPage}</a></li>`;
            else
                html += `<li class="page-item"><a class="page-link" href="${link}record=${pageSize}&page=${i}" onclick="ChangePage(${i},event,this)" title="Trang ${i}">${i}</a></a></li>`;

        if (currentPage < totalPage)
            html +=
                `<li>
                    <a class="page-link" href="${link}record=${pageSize}&page=${currentPage + 1}" title="Trang kế tiếp"  onclick="ChangePage(${currentPage + 1},event,this)" aria-label="Next">
                       <i class="uil uil-arrow-right"></i>
                    </a>
                </li>`;
    }
    $('#ul_main_pagination').html(html);
}

//Clear search keyword
function ClearSearchKeyword(elm) {
    $keywordEl.val('');
    $(elm).parent().remove();
    $pageEl.val(1);
    let record = $recordEl.val();

    //Change Link
    let newHref = `${GetLink(1, "keyword=", '')}record=${record}&page=1`;
    ChangeURLWithOut("", newHref); //not refresh page
    LoadListMainData();
}