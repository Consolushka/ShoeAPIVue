import {utils} from "./utils";
import axios from "axios";

export class Brand {
    Id
    Name
    ModelName = "Brand"

    constructor(brand) {
        if (brand === undefined) {
            this.Id = 0;
            this.Name = "";
            return;
        }
        this.Id = brand.id;
        this.Name = brand.name;
    }

    POST() {
        return axios.post(utils.API.BRANDS, {Name: this.Name}).then(response => response.status);
    }

    PUT() {
        return axios.put(utils.API.BRANDS + this.Id, this).then(response => response.status);
    }

    DELETE() {
        return axios.delete(utils.API.BRANDS + this.Id).then(response => response.status);
    }

    DELETEForce() {
        return axios.delete(utils.API.BRANDS + this.Id + "/" + "true").then(response => response.status);
    }
}

export class Shoe {
    Id
    Name
    Brand
    CreationTime
    PhotoFileName
    ModelName = "Shoe"
    matched = true;

    constructor(shoe) {
        if (shoe === undefined) {
            this.Id = 0;
            this.Name = "";
            this.Brand = new Brand();
            this.CreationTime = null;
            this.PhotoFileName = "undefined.jpg"
        } else {
            this.Id = shoe.Id;
            this.Name = shoe.Name;
            this.Brand = shoe.Brand;
            this.CreationTime = shoe.CreationTime;
            this.PhotoFileName = shoe.PhotoFileName;
        }
    }

    ToModel() {
        return {
            Id: this.Id,
            Name: this.Name,
            BrandId: this.Brand.Id,
            CreationTime: this.CreationTime,
            PhotoFileName: this.PhotoFileName
        }
    }

    MatchFilter(filterParam) {
        if (filterParam.BrandId !== null) {
            if (this.Brand.Id !== filterParam.BrandId) {
                this.matched = false;
                return;
            }
        }

        if (filterParam.Name !== null) {
            if (!this.Name.toLowerCase().includes(filterParam.Name.toLowerCase())) {
                this.matched = false;
                return;
            }
        }

        if (filterParam.CreationDate !== null) {
            if (Date.parse(this.CreationTime) !== Date.parse(filterParam.CreationDate + "T00:00:00")) {
                this.matched = false;
                return;
            }
        }
        this.matched = true;
        console.log(this, filterParam);
    }

    POST() {
        return axios.post(utils.API.SHOES, this.ToModel()).then(response => response.status);
    }

    PUT() {
        return axios.put(utils.API.SHOES + this.Id, this.ToModel()).then(response => response.status);
    }

    DELETE() {
        return axios.delete(utils.API.SHOES + this.Id).then(response => response.status);
    }
}

export class User {
    Id
    Email
    Password
    ConfirmPassword

    constructor(user) {
        if (user === undefined) {
            user = {
                Email: "",
                Password: "",
                ConfirmPassword: ""
            }
        }
        this.Email = user.Email;
        this.Password = user.Password;
        this.ConfirmPassword = user.ConfirmPassword
    }

    LogIn(){
        console.log(this);
        axios.post(utils.API.USER + "Authenticate", this).then((response) => {
            console.log(response);
        });
    }

    SingUp(){
        console.log(this);
        return axios.post(utils.API.USER + "Register", this).then(response => response);
    }

    LogOut(){
        return axios.post(utils.API.USER + "Logout").then(response => response.status);
    }

    GET(){
        axios.get(utils.API.USER + "GetUser").then((response) =>{
            console.log(response);
        })
            .catch((err)=>{
                console.log(err.response.data);
            });
    }
}
