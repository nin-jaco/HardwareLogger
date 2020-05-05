import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'http://localhost:5000/api/Hardware',
  json: true
})

export default {
  async execute(method, resource, data) {
    
    return client({
      method,
      url: resource,
      data,
      headers: {
        'Access-Control-Allow-Origin': '*',
      }
    }).then(req => {
      return req.data
    })
  },
  getAll() {
    return this.execute('get', '/')
  },
  create(data) {
    return this.execute('post', '/', data)
  },
  update(id, data) {
    return this.execute('put', `/${id}`, data)
  },
  delete(id) {
    return this.execute('delete', `/${id}`)
  }
}
