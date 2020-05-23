Hierarchy Organisation

Actors: All entities which could be considered "Characters", such as the player or NPC's,
are Actors, and should be childed to the Actors object.

Lighting: All lighting objects (Point Light, Directional Light, etc.) should be childed to this object
to keep all lighting objects together.

Level Geometry is split into 2 subsections. Static, and Dynamic.

Static objects have NO behaviours other than their colliders. They are merely part of the environment
and they DO NOT move.

Dynamic objects can have behaviours attatched, such as moving platforms or jump pads. Physics enabled objects
also fall under this category. If need be, these subsections can be further split to aid in organisation, such as
putting all Dynamic objects within one room in a folder for that room, looking something like this:

Level Geometry -> Dynamic -> Room 1

It's helpful if Static and Dynamic objects are not confused. Essentially, we should only ever have to place
a static object once and then forget about it, as it is only a collidable object and therefore of little
interest to the player.

All objects that could be considered part of the UI, or that only affect the UI, should be childed to the
UI object. This object should really be called "Canvas", but I decided to leave it as UI for now, considering
that a single scene can have multiple Canvas objects (Such as a specific Pause Menu Canvas.)

Objects that do not fall into any of these categories (like the Camera or Management scripts) should be placed
loosely in the hierarchy, not in any of the category Empty objects.

Please ensure when creating these objects that they are placed at position 0,0,0. Child object positions are
relative to their parent, not World Position, so if the parent is not at 0,0,0 it will move child objects.