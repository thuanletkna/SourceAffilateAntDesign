$(document).ready(function () {
    loadDataListCategoryPost();
    loadDataListCategoryProduct();
    listProductHome();
    listPostHome();

    
});

// Load danh sách danh mục con của sản phẩm
function loadDataListCategoryProduct() {
    var html = "";
    $.ajax({
        url: 'Categories/GetCategoryById',
        type: "GET",
        dataType: "json",
        success: function (response) {
            //var data = response.data;
            $.each(response, function (i, item) {
                html +=
                    '<li id="' + item.id +'" data-filter="home" class="btn">' +
                    '<input type="radio">' +
                    '<a onclick="listProductHome(' + item.id + ')" class="site-button-secondry">' + item.categoryName + '</a>' +
                    '</li>';
            });
            $('#loadCategoryProduct').html(html);
            $("li.btn").first().addClass("active");
        }
    })
}


// Load danh sách sản phẩm từ danh mục con của từng danh mục
function listProductHome(id) {
    var html = "";
    var url = "https://localhost:44325";

    $(".btn").removeClass("active");
    $("#"+id+"").addClass("active")

    $.ajax({
        url: "/AjaxApi/GetProductByCategoryIdAjax?id" + id,
        type: "GET",
        data: { id: id },
        dataType: "json",
        success: function (data) {

            $.each(data, function (i, item) {
                html +=
                    '<li class="home card-container col-lg-3 col-md-3 col-sm-6 col-6">' +
                    '                                <div class="dez-box dez-gallery-box">' +
                '                                    <div class="dez-media"> <a href="/product/' + item.seoAlias + '-' + item.id + '"><img src="https://admin.vuonhoala.vn' + item.imageProducts + ' "style="width:376px; height:236px" alt="' + item.title + '"></a> </div>' +
                    '                                    <div class="dez-info p-a20 text-center bg-gray" style="background-color: #daeded;">' +
                    '                                        <div class="p-lr20">' +
                    '                                            <h4 style="padding: 10px;" class="m-a0 bg-primary service-head"><a href="javascript:void(0);">' + item.categoryName + '</a></h4>' +
                    '                                        </div>' +
                    '                                        <h4 class="dez-title m-t0"><a href="/product/' + item.seoAlias + '-' + item.id + '" style="overflow: hidden;text-overflow: ellipsis;-webkit-line-clamp: 2;display: -webkit-box;-webkit-box-orient: vertical;">' + item.title + '</a></h4>' +
                    '                                        <div style="font-size: initial;" class="m-b0"><span>Giá Gốc: </span> <del style="font-size: initial;" class="m-r10"><span>' + item.price + ' VNĐ</span></del> </div>' +
                    '                                        <h3 class="m-b0"><span>Giá Bán:</span> <span style="font-size:20px; color:red">' + item.price + ' VNĐ</span> </h3>' +
                    '                                        <div class="m-t20">' +
                    '                                            <a href="' + item.linkAffilate + '" class="site-button radius-xl">Mua ngay</a>' +
                    '                                        </div>' +
                    '                                    </div>' +
                    '                                </div>' +
                    '                            </li>';
            });
            $('#masonryV2').html(html);
        }
    });
}

function loadDataListCategoryPost() {
    var html = "";
    $.ajax({
        url: 'AjaxApi/GetPostCategoryById',
        type: "GET",
        dataType: "json",
        success: function (response) {
            $.each(response, function (i, item) {
                html +=
                    '<li class="nav-item">' +
                    '<a  data-bs-toggle="tab" href="#" onclick="listPostHome(' + item.id + ')" class="nav-link">' + item.categoryName + '</a>' +
                    '</li>';

            });
            $('#loadCategoryPost').html(html);
            $(".nav-item:nth-child(1) .nav-link").addClass("active");

        }
    })
}

function listPostHome(id) {
    var html = "";
    var url = "https://localhost:44325";
    //$(".nav-link").removeClass("active");
    //$(".nav-link" + id + "").addClass("active")
    $.ajax({
        url: "/AjaxApi/GetPostByCategoryIdAjax?id" + id,
        type: "GET",
        data: { id: id },
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
                html +=
                    '<div class="blog-post blog-md clearfix">' +
                '                                            <div class="dez-post-media-style dez-img-effect zoom-slow"> <a href="/details/' + item.seoAlias + '-' + item.id + '"><img src="https://admin.vuonhoala.vn' + item.imagePost + '" style="width:250px; height:150px" alt="' + item.title + '"></a> </div>' +
                    '                                            <div class="dez-post-info" >' +
                    '                                                <div class="dez-post-title ">' +
                '                                                    <h5 style="overflow: hidden;text-overflow: ellipsis;-webkit-line-clamp: 1;display: -webkit-box;-webkit-box-orient: vertical;" class="post-title"><a href="/details/' + item.seoAlias + '-' + item.id + '">' + item.title + '</a></h5>' +
                    '                                                </div>' +
                    '                                                <div class="dez-post-meta ">' +
                    '                                                    <ul>' +
                    '                                                        <li class="post-date"> <i class="far fa-calendar-alt"></i>'+item.createDate+' </li>' +
                    '                                                    </ul>' +
                    '                                                </div>' +
                    '                                                <div class="dez-post-title">' +
                '                                                    <div style=" font-size: 14px;overflow: hidden;text-overflow: ellipsis;-webkit-line-clamp: 2;display: -webkit-box;-webkit-box-orient: vertical;"> '+ item.description +'</div>' +
                    '                                                </div>' +
                    '                                                <div class="dez-post-readmore">' +
                '                                                    <a href="/details/' + item.seoAlias + '-' + item.id + '" title="' + item.title + '" rel="bookmark" class="site-button-link">Xem thêm...</a>' +
                    '                                                </div>' +
                    '' +
                    '                                            </div>' +
                    '                                        </div>';
            });
            $('#masonryV1').html(html);
        }
    });
}