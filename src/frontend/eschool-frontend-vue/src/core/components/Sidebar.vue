<template>
  <q-list>
    <q-item v-ripple clickable>
      <q-item-section avatar>
        <q-icon name="home" />
      </q-item-section>
      <q-item-section>Kezdőlap</q-item-section>
    </q-item>
    <q-item v-ripple clickable>
      <q-item-section avatar>
        <q-icon name="email" />
      </q-item-section>
      <q-item-section>Üzeneteim</q-item-section>
    </q-item>

    <q-expansion-item
      icon="edit"
      label="Adminisztráció"
    >
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Tantárgyak</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Osztályok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Tagozatok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Tanévek</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Termek</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Órák</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      icon="description"
      label="Osztálynapló"
    >
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Órák</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Jegyek</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Hiányzások</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      icon="home"
      label="Házi feladatok"
    >
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Házi feladatok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      icon="quiz"
      label="Dolgozatok"
    >
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Dolgozatok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>
  </q-list>
</template>
