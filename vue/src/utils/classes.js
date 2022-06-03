import {utils} from "./utils";
import axios from "axios";

export class Brand {
    Id
    Name
    ModelName = "Brand"

    constructor(brand) {
        if (brand === undefined) {
            brand = {
                id: 0,
                Name: "all",
            }
        }
        this.Id = brand.id;
        this.Name = brand.name;
    }

    ToModel(){
        return {
            id: this.Id,
            name: this.Name
        }
    }

    POST(config) {
        return axios.post(utils.API.BRANDS+"Add", this, config)
            .then(response => response)
            .catch(err => err.response);
    }

    PUT(config) {
        return axios.put(utils.API.BRANDS + "Update", this, config)
            .then(response => response)
            .catch(err=>err.response);
    }

    DELETE(config) {
        return axios.delete(utils.API.BRANDS + "Delete/"+this.Id, config).then(response => response.status);
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
            this.Id = shoe.id;
            this.Name = shoe.name;
            this.Brand = new Brand(shoe.brand);
            this.CreationTime = shoe.creationTime;
            this.PhotoFileName = shoe.photoFileName;
        }
    }

    ToModel() {
        return {
            id: this.Id,
            name: this.Name,
            brandId: this.Brand.Id,
            creationTime: this.CreationTime,
            photoFileName: this.PhotoFileName
        }
    }

    MatchFilter(filterParam) {
        if (filterParam.BrandId !== null && filterParam.BrandId !== 0) {
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
            if (!(Date.parse(this.CreationTime) >= Date.parse(filterParam.CreationDate[0] + "T00:00:00") && Date.parse(this.CreationTime) <= Date.parse(filterParam.CreationDate[1] + "T00:00:00"))) {
                this.matched = false;
                return;
            }
        }
        this.matched = true;
    }

    POST(config) {
        return axios.post(utils.API.GOODS+"Add", this.ToModel(), config)
            .then(response => response)
            .catch(err=>err.response);
    }

    PUT(config) {
        return axios.put(utils.API.GOODS + "Update", this, config)
            .then(response => response.status)
            .catch(err=>err.response);
    }

    DELETE(config) {
        return axios.delete(utils.API.GOODS +"Delete/" + this.Id, config).then(response => response.status);
    }
}

export class User {
    Id
    Login
    Address
    Email
    UserName
    Password
    RoleId
    token
    IsConfirmed

    constructor(user) {
        if (user === undefined) {
            user = {
                id: 0,
                email: "",
                userName: "",
                password: "",
                token: "",
                roleId: 1,
                isConfirmed: false
            }
        }
        this.Id = user.id;
        this.Email = user.email;
        this.Address = user.address;
        this.UserName = user.userName;
        this.Password = user.Password;
        this.token = user.token;
        this.RoleId = user.roleId;
        this.IsConfirmed = user.isConfirmed;
    }

    ToModel(){
        return {
            email: this.Email,
            password: this.Password
        };
    }

    Register(){
        return axios.post(utils.API.USER+"register", this)
            .then(response => response)
            .catch(err => err.response);
    }

    Authenticate(){
        this.Email = this.Login;
        this.UserName = this.Login;
        return axios.post(utils.API.USER+"authenticate", this)
            .then(response => response)
            .catch(err=>err.response);
    }

    Update(config){
        console.log(config);
        return axios.put(utils.API.USER+`update/${this.Id}`, this, config)
            .then(response => response)
            .catch(err=>err.response);
    }

    ChangePassword(pass, config){
        console.log(config);
        return axios.put(utils.API.USER+`change-password?password=${pass}&id=${this.Id}`,null, config)
            .then(response => response)
            .catch(err=>err.response);
    }
}
