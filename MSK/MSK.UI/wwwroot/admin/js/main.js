let deleteBtns = document.querySelectorAll(".delete-btn")
deleteBtns.forEach(btn => btn.addEventListener("click", (e) => {
    e.preventDefault();
    let url = btn.getAttribute("href");

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(url).then(res => {
                if (res.status == 200) {
                    window.location.reload(true);
                } else {
                    console.log(res);
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                    });
                }
            })
        }
    });
}))
//modal js in layout profile section 
// add the animation to the popover
//$('a[rel=popover]').popover().click(function (e) {
//    e.preventDefault();
//    var open = $(this).attr('data-easein');
//    if (open == 'shake') {
//        $(this).next().velocity('callout.' + open);
//    } else if (open == 'pulse') {
//        $(this).next().velocity('callout.' + open);
//    } else if (open == 'tada') {
//        $(this).next().velocity('callout.' + open);
//    } else if (open == 'flash') {
//        $(this).next().velocity('callout.' + open);
//    } else if (open == 'bounce') {
//        $(this).next().velocity('callout.' + open);
//    } else if (open == 'swing') {
//        $(this).next().velocity('callout.' + open);
//    } else {
//        $(this).next().velocity('transition.' + open);
//    }

//});

//// add the animation to the modal
//$(".modal").each(function (index) {
//    $(this).on('show.bs.modal', function (e) {
//        var open = $(this).attr('data-easein');
//        if (open == 'shake') {
//            $('.modal-dialog').velocity('callout.' + open);
//        } else if (open == 'pulse') {
//            $('.modal-dialog').velocity('callout.' + open);
//        } else if (open == 'tada') {
//            $('.modal-dialog').velocity('callout.' + open);
//        } else if (open == 'flash') {
//            $('.modal-dialog').velocity('callout.' + open);
//        } else if (open == 'bounce') {
//            $('.modal-dialog').velocity('callout.' + open);
//        } else if (open == 'swing') {
//            $('.modal-dialog').velocity('callout.' + open);
//        } else {
//            $('.modal-dialog').velocity('transition.' + open);
//        }

//    });
//});
