import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);
import app from './modules/app'
import session from './modules/session'
import account from './modules/account'
import user from './modules/user'
import role from './modules/role'
import tenant from './modules/tenant'
import student from './modules/student'
import record from './modules/record'
import order from './modules/order'
import course from './modules/course'
import courseItem from './modules/courseItem'
import contract from './modules/contract'
import customer from './modules/customer'
import lesson from './modules/lesson'
import teacher from './modules/teacher'
const store = new Vuex.Store({
    state: {
        //
    },
    mutations: {
        //
    },
    actions: {

    },
    modules: {
        app,
        session,
        account,
        user,
        role,
        tenant,
        student,
        record,
        order,
        course,
        courseItem,
        contract,
        customer,
        lesson,
        teacher
    }
});

export default store;