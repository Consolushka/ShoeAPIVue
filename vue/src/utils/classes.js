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

    POST(config) {
        return axios.post(utils.API.BRANDS+"Add", this, config).then(response => response.status);
    }

    PUT(config) {
        return axios.put(utils.API.BRANDS + "Update", this, config).then(response => response.status);
    }

    DELETE(config) {
        console.log(this.Id);
        return axios.delete(utils.API.BRANDS + "Delete/"+this.Id, config).then(response => response.status);
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
            this.Id = shoe.id;
            this.Name = shoe.name;
            this.Brand = new Brand(shoe.brand);
            this.CreationTime = shoe.creationTime;
            this.PhotoFileName = shoe.photoFileName;
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
        return axios.post(utils.API.SHOES, this).then(response => response.status);
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
    token

    constructor(user) {
        if (user === undefined) {
            user = {
                Email: "",
                Password: "",
                token: ""
            }
        }
        this.Email = user.email;
        this.Password = user.Password;
        this.token = user.token;
    }

    Register(){
        return axios.post(utils.API.USER+"register", this)
            .then(response => response.data);
    }

    Authenticate(){
        return axios.post(utils.API.USER+"authenticate", this)
            .then(response => response.data);
    }
}
