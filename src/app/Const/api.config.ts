import { Service } from "src/app/Interfaces/Service/service.interface";

export const apiConfig: Record<string, Service> = {
  login: {
    url: "account/",
    methods: {
      login: {
        url: "login/",
        type: "POST",
      },
      logout: {
        url: "logout/",
        type: "GET",
      },
      refreshtoken: {
        url: "refreshtoken/",
        type: "POST",
        loader: false,
      },
    },
  },
  tests: {
    url: "tests/",
    methods: {
      getall: {
        url: "getall/",
        type: "GET"
      },
      getbyid: {
        url: "getbyid/",
        type: "GET"
      },
      start: {
        url: "start/",
        type: "GET"
      },
      getsessionbyid: {
        url: "getsessionbyid/",
        type: "GET"
      },
      setquestionoption: {
        url: "setquestionoption/",
        type: "POST"
      },
      endsessionbyid: {
        url: "endsessionbyid/",
        type: "GET"
      }
    }
  }
};
