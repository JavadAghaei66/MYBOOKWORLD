 /*=============== SEARCH ===============*/
 const search = document.getElementById("search"),
 searchBtn = document.getElementById("search-btn"),
 searchClose = document.getElementById("search-close");

/* Search show */
searchBtn.addEventListener("click", () => {
 search.classList.add("show-search");
});

/* Search hidden */
searchClose.addEventListener("click", () => {
 search.classList.remove("show-search");
});

/*=============== LOGIN ===============*/
const login = document.getElementById("login"),
 loginBtn = document.getElementById("login-btn"),
 loginClose = document.getElementById("login-close");

/* Login show */
loginBtn.addEventListener("click", () => {
 login.classList.add("show-login");
});

/* Login hidden */
loginClose.addEventListener("click", () => {
 login.classList.remove("show-login");
});