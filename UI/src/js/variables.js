const ROOT = "http://localhost:5000/";

export const variables = {
  API: {
    BRANDS: `${ROOT}api/brands/`,
    SHOES:`${ROOT}api/shoes/`
  },
  PHOTO_URL: `${ROOT}photos/`,
  CloseModal(type, model) {
    document.querySelector(`#${type}${model}Modal`).classList.remove("show");
    document.querySelector(".modal-backdrop").classList.remove("show");
    document.querySelector("body").classList.remove("modal-open");
    document.querySelector("body").removeAttribute("style");
  }
}
