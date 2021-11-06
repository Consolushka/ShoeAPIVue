import {utils} from "./utils";
import axios from "axios";

export class Brand {
    Id
    Name
    ModelName = "Brand"

    constructor(brand) {
        if (brand === undefined) {
            this.Id = 0;
            this.Name = "all";
            return;
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
        return axios.post(utils.API.SHOES+"Add", this.ToModel(), config)
            .then(response => response)
            .catch(err=>err.response);
    }

    PUT(config) {
        return axios.put(utils.API.SHOES + "Update", this, config)
            .then(response => response.status)
            .catch(err=>err.response);
    }

    DELETE(config) {
        return axios.delete(utils.API.SHOES +"Delete/" + this.Id, config).then(response => response.status);
    }
}

export class User {
    Id
    Email
    Password
    RoleId
    token
    IsConfirmed

    constructor(user) {
        if (user === undefined) {
            user = {
                email: "",
                password: "",
                token: "",
                roleId: 1,
                isConfirmed: false
            }
        }
        this.Email = user.email;
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
        return axios.post(utils.API.USER+"authenticate", this)
            .then(response => response)
            .catch(err=>err.response);
    }
}
