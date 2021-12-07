# Run With Docker Containers

## Dev mode

Generate a certificate file named idp.pfx in the cert folder of this folder with the following command:

```
dotnet dev-certs https -ep ./cert/idp.pfx -p Password123 -t
```

Run the compose project with the following command:

```
docker-compose -f docker-compose-with-frontend.yml up
```

**Warning:** At the current version of Quasar the dialog components do not work properly when used with the `<script setup>` tag. Unfortunately I used it in my projects so running the frontend in production mode will result in dialogs not showing. The problem can be solved by rewriting all of the dialogs to not use the setup tag.

To avoid the problem run the backend in docker-compose:

```
docker-compose up
```

And start a local dev server for the frontend.

```
cd src/frontend/eschool-frontend-vue
npm install
npm run serve
```
