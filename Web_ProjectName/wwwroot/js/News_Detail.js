
    document.addEventListener('DOMContentLoaded', function() {
        // Xử lý click vào các section
        document.querySelectorAll('.click-to-focus').forEach(element => {
            element.addEventListener('click', function (e) {
                e.stopPropagation();

                // Loại bỏ focus cũ
                document.querySelectorAll('.focused-section').forEach(el => {
                    el.classList.remove('focused-section');
                });

                // Thêm focus mới
                this.classList.add('focused-section');

                // Scroll đến vị trí phù hợp
                const elementRect = this.getBoundingClientRect();
                const absoluteElementTop = elementRect.top + window.pageYOffset;
                const middle = absoluteElementTop - (window.innerHeight / 2) + (elementRect.height / 2);
                window.scrollTo({
                    top: middle,
                    behavior: 'smooth'
                });
            });
        });

        // Xử lý click vào card section
        document.querySelectorAll('.focus-section').forEach(section => {
        section.addEventListener('click', function () {
            // Loại bỏ focus cũ
            document.querySelectorAll('.focused-section').forEach(el => {
                el.classList.remove('focused-section');
            });

            // Scroll to section
            this.scrollIntoView({
                behavior: 'smooth',
                block: 'center'
            });

            // Highlight section
            this.classList.add('focused-section');
            setTimeout(() => {
                this.classList.remove('focused-section');
            }, 2000);
        });
        });
    });
