function showPhoneInput() {
    const buttonsContainer = document.querySelector('.login-page__buttons');
    const backArrow = document.getElementById('back-arrow');
    backArrow.style.display = 'block';
    buttonsContainer.innerHTML = `
                    <input id="phone" type="tel" placeholder="Nhập số điện thoại" class="form-control mb-3" />
                    <button class="btn btn-primary w-100">Đăng Nhập</button>
                `;

    // Khởi tạo intl-tel-input cho trường số điện thoại
    const phoneInput = document.querySelector("#phone");
    window.intlTelInput(phoneInput, {
        initialCountry: "vn", // Quốc gia mặc định là Việt Nam
        preferredCountries: ["vn", "us", "gb"], // Các quốc gia ưu tiên
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js"
    });
}

function showUsernameInput() {
    const buttonsContainer = document.querySelector('.login-page__buttons');
    const formlogin = document.getElementById('formlogin');
    const backArrow = document.getElementById('back-arrow');

    backArrow.style.display = 'block';
    buttonsContainer.innerHTML = `
                    <input type="text" placeholder="Nhập tài khoản" class="form-control mb-3" />
                    <input type="password" placeholder="Nhập mật khẩu" class="form-control mb-3" />
                    <button class="btn btn-primary w-100">Đăng Nhập</button>
                `;
    document.getElementById('username-button').addEventListener('click', () => {
        formlogin.style.height = '550px !important';
    });

}

function resetForm() {
    const buttonsContainer = document.querySelector('.login-page__buttons');
    const backArrow = document.getElementById('back-arrow');
    backArrow.style.display = 'none';
    buttonsContainer.innerHTML = `
                    <button id="phone-button" onclick="showPhoneInput()">Tiếp Tục Bằng Số Điện Thoại</button>
                    <button id="username-button" onclick="showUsernameInput()">Tiếp Tục Bằng Username</button>
                `;
}